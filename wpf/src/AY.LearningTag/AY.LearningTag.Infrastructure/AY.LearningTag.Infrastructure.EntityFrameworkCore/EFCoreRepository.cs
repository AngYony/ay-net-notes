using AY.LearningTag.Domain.Abstractions.Entities;
using AY.LearningTag.Domain.Abstractions.Repositories;
using AY.LearningTag.Domain.EFCore.Repositories.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AY.LearningTag.Infrastructure.EntityFrameworkCore
{
    /// <summary>
    /// 通用的EF Core仓储实现
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TDbContext"></typeparam>
    public class EFCoreRepository<TEntity, TDbContext> : IEFCoreRepository<TEntity>
        where TEntity : class, IEntity
        where TDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        protected readonly TDbContext dbContext;
        protected readonly DbSet<TEntity> Table;

        /*
         * IDbContextFactory<T> 的默认注入生命周期是 Singleton（单例）。
         * 其本质上是一个“工厂工具”，它的职责是根据配置信息（如数据库连接字符串）随时生产出新的 DbContext 实例。
         */
        public EFCoreRepository(IDbContextFactory<TDbContext> factory)
        {
            dbContext = factory.CreateDbContext(); //Repository 生命周期 = DbContext 生命周期
            Table = dbContext.Set<TEntity>();
        }

        /// <summary>
        /// 根查询，已启用非跟踪的标识解析
        /// <para>如果需要进一步改善复杂查询（特别是连接查询）的性能，推荐调用AsSplitQuery方法启用拆分SQL的查询</para>
        /// 不跟踪、但会做“实例去重”、同一个主键只生成一个对象
        /// </summary>
        public virtual IQueryable<TEntity> Query => Table.AsNoTrackingWithIdentityResolution();

        public IQueryable<TEntity> GetIQueryable()
        {
            return Table.AsQueryable();
        }

        #region Insert 操作
        /// <summary>
        /// 新增单个实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual TEntity Insert(TEntity entity) => Table.Add(entity).Entity;

        /// <summary>
        /// 自动保存添加单个实体
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="autoSave"></param>
        /// <returns></returns>
        public virtual TEntity Insert(TEntity entity, bool autoSave = false)
        {
            var saveEntity = Insert(entity);
            if (autoSave)
            {
                dbContext.SaveChanges();
            }
            return saveEntity;
        }

        /// <summary>
        /// 新增单个实体异步方法
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            var task = await Table.AddAsync(entity, cancellationToken);
            return task.Entity;
        }

        /// <summary>
        /// 自动保存添加单个实体的异步方法
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="autoSave"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<TEntity> InsertAsync(TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            var saveEntity = await InsertAsync(entity, cancellationToken);
            if (autoSave)
            {
                await dbContext.SaveChangesAsync(cancellationToken);
            }
            return saveEntity;
        }

        /// <summary>
        /// 同时新增多个实体
        /// </summary>
        /// <param name="entities"></param>
        public virtual void InsertMany(IEnumerable<TEntity> entities) => Table.AddRange(entities);

        /// <summary>
        /// 自动保存同时新增多个实体
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="autoSave"></param>
        public virtual void InsertMany(IEnumerable<TEntity> entities, bool autoSave = false)
        {
            InsertMany(entities);
            if (autoSave)
            {
                dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// 同时新增多个实体异步方法
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task InsertManyAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
            => Table.AddRangeAsync(entities, cancellationToken);

        /// <summary>
        /// 自动保存新增多个实体
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="autoSave"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task InsertManyAsync(IEnumerable<TEntity> entities, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            await InsertManyAsync(entities, cancellationToken);
            if (autoSave)
            {
                await dbContext.SaveChangesAsync(cancellationToken);
            }
        }

        #endregion

        #region Delete

        /// <summary>
        /// 删除单个实体
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Delete(TEntity? entity)
        {
            if (entity is not null)
            {
                Table.Remove(entity);
            }
        }

        public virtual void Delete(TEntity? entity, bool autoSave = false)
        {
            if (entity is not null)
            {
                Table.Remove(entity);
                if (autoSave)
                {
                    dbContext.SaveChanges();
                }
            }
        }

        /// <summary>
        /// 异步删除单个实体（自动SaveChanges）
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            Delete(entity); //没有异步方法，只有在SaveChanges时异步
            return dbContext.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// 根据条件删除多个实体
        /// </summary>
        /// <param name="predicate"></param>
        public virtual void Delete(Expression<Func<TEntity, bool>> predicate)
        {
            var entities = GetIQueryable().Where(predicate).ToList();
            DeleteMany(entities);
        }

        public virtual void Delete(Expression<Func<TEntity, bool>> predicate, bool autoSave = false)
        {
            Delete(predicate);
            if (autoSave)
            {
                dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// 根据条件异步删除多个实体
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual Task DeleteAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            var entities = GetIQueryable().Where(predicate).ToList();
            return DeleteManyAsync(entities, cancellationToken);
        }

        /// <summary>
        /// 同步删除多个实体
        /// </summary>
        /// <param name="entities"></param>
        public virtual void DeleteMany(IEnumerable<TEntity> entities)
        {
            Table.RemoveRange(entities);
        }

        public virtual void DeleteMany(IEnumerable<TEntity> entities, bool autoSave = false)
        {
            DeleteMany(entities);
            if (autoSave)
            {
                dbContext.SaveChanges();
            }
        }
        /// <summary>
        /// 异步删除多个实体（自动SaveChangesAsync）
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task DeleteManyAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            Table.RemoveRange(entities);
            return dbContext.SaveChangesAsync(cancellationToken);
        }


        #endregion

        #region Update

        public virtual TEntity Update(TEntity entity)
        {
            dbContext.Attach(entity);
            var updatedEntity = Table.Update(entity).Entity;
            return updatedEntity;
        }

        public virtual TEntity Update(TEntity entity, bool autoSave = false)
        {
            var updatedEntity = Update(entity);
            if (autoSave)
            {
                dbContext.SaveChanges();
            }
            return updatedEntity;
        }

        /// <summary>
        /// 更新单个实体（自动SaveChanges）
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            var updatedEntity = Update(entity);
            await dbContext.SaveChangesAsync(cancellationToken);
            return updatedEntity;
        }

        public virtual void UpdateMany(IEnumerable<TEntity> entities) => Table.UpdateRange(entities);

        public virtual void UpdateMany(IEnumerable<TEntity> entities, bool autoSave = false)
        {
            UpdateMany(entities);
            if (autoSave)
            {
                dbContext.SaveChanges();
            }
        }
        /// <summary>
        /// 更新多个实体，自动SaveChanges
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task UpdateManyAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            UpdateMany(entities);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
        #endregion

        #region Select

        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return GetIQueryable().FirstOrDefault(predicate);
        }

        public Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return GetIQueryable().FirstOrDefaultAsync(predicate, cancellationToken);
        }

        public TEntity Single(Expression<Func<TEntity, bool>> predicate)
        {
            return GetIQueryable().SingleOrDefault(predicate);
        }

        public async Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await GetIQueryable().SingleOrDefaultAsync(predicate, cancellationToken);
        }

        public List<TEntity> GetAllList()
        {
            return GetIQueryable().ToList();
        }

        public async Task<List<TEntity>> GetAllListAsync(CancellationToken cancellationToken = default)
        {
            return await GetIQueryable().ToListAsync(cancellationToken);
        }

        public List<TEntity> GetList(Expression<Func<TEntity, bool>> predicate)
        {
            return GetIQueryable().Where(predicate).ToList();
        }

        public async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await GetIQueryable().Where(predicate).ToListAsync(cancellationToken);
        }

        public int GetCount()
        {
            return GetIQueryable().Count();
        }


        public int GetCount(Expression<Func<TEntity, bool>> predicate)
        {
            return GetIQueryable().Where(predicate).Count();
        }

        public async Task<int> GetCountAsync(CancellationToken cancellationToken = default)
        {
            return await GetIQueryable().CountAsync(cancellationToken);
        }

        public async Task<int> GetCountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await GetIQueryable().Where(predicate).CountAsync(cancellationToken);
        }

        public long GetLongCount()
        {
            return GetIQueryable().LongCount();
        }

        public async Task<long> GetLongCountAsync(CancellationToken cancellationToken = default)
        {
            return await GetIQueryable().LongCountAsync(cancellationToken);
        }

        public long GetLongCount(Expression<Func<TEntity, bool>> predicate)
        {
            return GetIQueryable().Where(predicate).LongCount();
        }

        public async Task<long> GetLongCountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await GetIQueryable().Where(predicate).LongCountAsync(cancellationToken);
        }

        #endregion

        public void Attach(TEntity entity) => dbContext.Attach(entity);

        public void AttachRange(IEnumerable<TEntity> entities) => dbContext.AttachRange(entities);

        public void Detach(TEntity entity) => dbContext.Entry(entity).State = EntityState.Detached;

        public void DetachRange(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                Detach(entity);
            }
        }

        /// <summary>
        /// 重置（更改跟踪器的）状态
        /// </summary>
        public void ResetDbContext() => dbContext.ChangeTracker.Clear();

        public EntityEntry<TEntity> GetEntityEntry(TEntity entity) => dbContext.Entry(entity);

        public virtual int SaveChanges()
        {
            GenerateConcurrencyStamp();
            return dbContext.SaveChanges();
        }

        public virtual Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            GenerateConcurrencyStamp();
            return dbContext.SaveChangesAsync(cancellationToken);
        }


        /// <summary>
        /// 生成并发检查令牌
        /// </summary>
        protected virtual void GenerateConcurrencyStamp()
        {
            // 找到所有实现并发检查接口的实体
            var changedEnties = dbContext.ChangeTracker.Entries()
                .Where(x => x.Entity is IOptimisticConcurrencySupported && x.State is EntityState.Added or EntityState.Modified);

            foreach (var entry in changedEnties)
            {
                if (entry.State is EntityState.Added)
                {
                    (entry.Entity as IOptimisticConcurrencySupported)!.ConcurrencyStamp = Guid.NewGuid().ToString();
                }
                if (entry.State is EntityState.Modified)
                {
                    // 如果是更新实体，需要分别处理原值和新值
                    var concurrencyStamp = entry.Property(nameof(IOptimisticConcurrencySupported.ConcurrencyStamp));
                    // 实体的当前值要指定为原值
                    concurrencyStamp!.OriginalValue = (entry.Entity as IOptimisticConcurrencySupported)!.ConcurrencyStamp;
                    // 然后重新生成新值
                    concurrencyStamp.CurrentValue = Guid.NewGuid().ToString();
                }
            }
        }

        public void Dispose() => dbContext?.Dispose();

        public ValueTask DisposeAsync() => dbContext?.DisposeAsync() ?? ValueTask.CompletedTask;

        #region 私有方法

        ///// <summary>
        ///// 检查实体是否处于跟踪状态，如果是则返回，如果没有则添加跟踪状态
        ///// </summary>
        ///// <param name="entity"></param>
        //protected virtual void AttachIfNot(TEntity entity)
        //{
        //    var entry = dbContext.ChangeTracker.Entries()
        //        .FirstOrDefault(ent => ent.Entity == entity);

        //    if (entry != null)
        //    {
        //        return;
        //    }

        //    Table.Attach(entity);
        //}



        #endregion 私有方法


    }

    public class EFCoreRepository<TEntity, TPrimaryKey, TDbContext> :
        EFCoreRepository<TEntity, TDbContext>,
        IEFCoreRepository<TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
        where TPrimaryKey : IEquatable<TPrimaryKey>
        where TDbContext : DbContext
    {
        public EFCoreRepository(IDbContextFactory<TDbContext> factory) : base(factory)
        {
        }

        public virtual void Delete(TPrimaryKey key)
        {
            var entity = Single(key);
            Delete(entity);
        }

        public virtual Task DeleteAsync(TPrimaryKey key, CancellationToken cancellationToken = default)
        {
            Delete(key);
            return Task.CompletedTask;
        }


        public virtual void DeleteMany(IEnumerable<TPrimaryKey> keys)
        {
            var entities = Single(keys).ToArray();
            Table.AttachRange(entities);
            DeleteMany(entities);
        }


        public virtual Task DeleteManyAsync(IEnumerable<TPrimaryKey> keys, CancellationToken cancellationToken = default)
        {
            DeleteMany(keys);
            return Task.CompletedTask;
        }

        public virtual TEntity? Single(TPrimaryKey key)
        {
            return Query.SingleOrDefault(x => x.Id.Equals(key));
        }

        public virtual Task<TEntity?> SingleAsync(TPrimaryKey key, CancellationToken cancellationToken = default)
        {
            return Query.SingleOrDefaultAsync(x => x.Id.Equals(key), cancellationToken);
        }

        public virtual IQueryable<TEntity> Single(IEnumerable<TPrimaryKey> keys)
        {
            return Query.Where(x => keys.Contains(x.Id));
        }

    }
}
