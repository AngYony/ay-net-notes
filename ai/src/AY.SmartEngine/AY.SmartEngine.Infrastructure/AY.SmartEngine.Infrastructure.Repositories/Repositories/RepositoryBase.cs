using AY.SmartEngine.Domain.Entities;
using AY.SmartEngine.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace AY.SmartEngine.Infrastructure.Repositories.Repositories
{
    /// <summary>
    /// 基于 EF Core 10 + Sqlite 的泛型仓储实现
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TDbContext"></typeparam>
    public class RepositoryBase<TEntity, TDbContext> : IRepositoryBase<TEntity>
        where TEntity : BaseEntity
        where TDbContext : DbContext
    {
        protected readonly IDbContextFactory<TDbContext> _dbContextFactory;

        public RepositoryBase(IDbContextFactory<TDbContext> dbContextFactory)
        {
            this._dbContextFactory = dbContextFactory;
        }

        #region 新增操作

        /// <summary>
        /// 写入数据并返回新写入的实体
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
            //使用 Add (内存操作)，只有 SaveChangesAsync 是真正的 I/O 异步
            context.Set<TEntity>().Add(entity);
            //此时 SaveChanges 会填充自增 ID 到 entity 对象中
            await context.SaveChangesAsync(cancellationToken);
            return entity;
        }

        /// <summary>
        /// 批量插入数据
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
            await context.Set<TEntity>().AddRangeAsync(entities, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
        }

        #endregion 新增操作

        #region 删除操作

        /// <summary>
        /// 物理删除
        /// </summary>
        public virtual async Task<bool> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
            context.Set<TEntity>().Remove(entity);
            return await context.SaveChangesAsync(cancellationToken) > 0;
        }

        /// <summary>
        /// 根据Id删除数据
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            return await DeleteAsync(x => x.Id == id, cancellationToken) > 0;
        }

        /// <summary>
        /// 批量物理删除 (高效：直接生成 SQL DELETE)
        /// </summary>
        public virtual async Task<int> DeleteAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
            return await context.Set<TEntity>().Where(predicate).ExecuteDeleteAsync(cancellationToken);
        }

        /// <summary>
        /// 逻辑删除 (软删除)
        /// </summary>
        public virtual async Task<int> SoftDeleteAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken ct = default)
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync(ct);
            return await context.Set<TEntity>()
                .Where(predicate)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(b => b.IsDeleted, true)
                    .SetProperty(b => b.UpdatedAt, DateTime.Now),
                ct);
        }

        #endregion 删除操作

        #region 修改操作

        /// <summary>
        /// 全量更新实体
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<bool> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
            // 更新审计时间
            entity.UpdatedAt = DateTime.Now;
            context.Set<TEntity>().Update(entity);
            // 处理并发冲突
            try
            {
                return await context.SaveChangesAsync(cancellationToken) > 0;
            }
            catch (DbUpdateConcurrencyException)
            {
                // 可以在这里打日志或抛出自定义异常
                throw;
            }
        }

        /// <summary>
        /// 批量更新某些字段 (适配 EF Core 9/10 语法)(高效：直接生成 SQL UPDATE)
        /// </summary>
        /// <param name="predicate">过滤条件</param>
        /// <param name="setPropertyCalls">更新字段的动作</param>
        /// <example>
        /// await repo.UpdateAsync(x => x.Id == 1, s => s.SetProperty(x => x.Name, "NewName"));
        /// </example>
        public virtual async Task<int> UpdateAsync(
            Expression<Func<TEntity, bool>> predicate,
            Action<UpdateSettersBuilder<TEntity>> setPropertyCalls, // 改为 Action
            CancellationToken cancellationToken = default)
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
            return await context.Set<TEntity>()
                .Where(predicate)
                .ExecuteUpdateAsync(setPropertyCalls, cancellationToken);
        }

        #endregion 修改操作

        #region 查询操作

        /// <summary>
        /// 根据主键获取实体
        /// </summary>
        public virtual async Task<TEntity?> GetFirstAsync(int id, CancellationToken cancellationToken = default)
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
            return await context.Set<TEntity>().FindAsync(new object[] { id }, cancellationToken);
        }

        /// <summary>
        /// 获取单条数据 (支持条件)
        /// </summary>
        public virtual async Task<TEntity?> GetFirstAsync(
            Expression<Func<TEntity, bool>> predicate, bool tracking = false, CancellationToken cancellationToken = default)
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
            var query = context.Set<TEntity>().AsQueryable();
            if (!tracking) query = query.AsNoTracking();
            return await query.FirstOrDefaultAsync(predicate, cancellationToken);
        }

        /// <summary>
        /// 基础条件查询 (List)
        /// </summary>
        public virtual async Task<List<TEntity>> GetListAsync(
            Expression<Func<TEntity, bool>>? predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            CancellationToken cancellationToken = default)
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
            IQueryable<TEntity> query = context.Set<TEntity>().AsNoTracking();
            if (predicate != null) query = query.Where(predicate);
            if (orderBy != null) query = orderBy(query);
            return await query.ToListAsync(cancellationToken);
        }

        /// <summary>
        /// 条件查询并支持分页与排序
        /// </summary>
        public virtual async Task<(List<TEntity> Items, int Total)> GetPagedListAsync(
            int pageIndex,
            int pageSize,
            Expression<Func<TEntity, bool>>? predicate = null,
            Expression<Func<TEntity, object>>? orderBy = null,
            bool isDescending = true,
            CancellationToken cancellationToken = default)
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
            var query = context.Set<TEntity>().AsNoTracking();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            int total = await query.CountAsync(cancellationToken);

            if (orderBy != null)
            {
                query = isDescending ? query.OrderByDescending(orderBy) : query.OrderBy(orderBy);
            }
            else
            {
                // SQLite 分页建议默认主键排序，防止乱序
                query = query.OrderBy(x => x.Id);
            }

            var items = await query.Skip((pageIndex - 1) * pageSize)
                                   .Take(pageSize)
                                   .ToListAsync(cancellationToken);

            return (items, total);
        }

        #endregion 查询操作

        #region 工具方法

        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public virtual async Task<bool> ExistsAsync(
            Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
            return await context.Set<TEntity>().AnyAsync(predicate, cancellationToken);
        }

        /// <summary>
        /// 获取数量
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="ct"></param>
        /// <returns></returns>

        public virtual async Task<int> CountAsync(
            Expression<Func<TEntity, bool>>? predicate = null, CancellationToken ct = default)
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync(ct);
            return predicate != null
                ? await context.Set<TEntity>().CountAsync(predicate, ct)
                : await context.Set<TEntity>().CountAsync(ct);
        }

        #endregion 工具方法
    }
}