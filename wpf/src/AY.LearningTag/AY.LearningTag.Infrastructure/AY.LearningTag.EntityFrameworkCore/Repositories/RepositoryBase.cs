using AY.LearningTag.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace WY.EntityFramework.Repositories
{
    /// <summary>
    /// 默认仓储的通用功能实现，用于所有的领域模型
    /// </summary>
    /// <typeparam name="TEntity"></typeparam> 

    public class RepositoryBase<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey>
    where TEntity : class
    {
        /// <summary>
        /// 数据库上下文
        /// </summary>
        protected readonly LearningTagDbContext _dbContext;

        /// <summary>
        /// 通过泛型，从数据库上下文中获取领域模型
        /// </summary>
        public virtual DbSet<TEntity> Table => _dbContext.Set<TEntity>();

        public RepositoryBase(LearningTagDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<TEntity> GetIQueryable()
        {
            return Table.AsQueryable();
        }

        public List<TEntity> GetAllList()
        {
            return GetIQueryable().ToList();
        }

        public async Task<List<TEntity>> GetAllListAsync()
        {
            return await GetIQueryable().ToListAsync();
        }

        public List<TEntity> GetAllList(Expression<Func<TEntity, bool>> predicate)
        {
            return GetIQueryable().Where(predicate).ToList();
        }

        public async Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await GetIQueryable().Where(predicate).ToListAsync();
        }

        public TEntity Single(Expression<Func<TEntity, bool>> predicate)
        {
            return GetIQueryable().Single(predicate);
        }

        public async Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await GetIQueryable().SingleAsync(predicate);
        }

        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return GetIQueryable().FirstOrDefault(predicate);
        }

        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var entity = await GetIQueryable().FirstOrDefaultAsync(predicate);
            return entity;
        }

        public TEntity Insert(TEntity entity)
        {
            var newEntity = Table.Add(entity).Entity;
            Save();

            return newEntity;
        }

        public async Task<TEntity> InsertAsync(TEntity entity)
        {
            var entityEntry = await Table.AddAsync(entity);

            await SaveAsync();
            return entityEntry.Entity;
        }

        public TEntity Update(TEntity entity)
        {
            AttachIfNot(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
            Save();
            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            AttachIfNot(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
            await SaveAsync();

            return entity;
        }

        public void Delete(TEntity entity)
        {
            AttachIfNot(entity);
            Table.Remove(entity);
            Save();
        }

        public async Task DeleteAsync(TEntity entity)
        {
            AttachIfNot(entity);
            Table.Remove(entity);
            await SaveAsync();
        }

        public void Delete(Expression<Func<TEntity, bool>> predicate)
        {
            foreach (var entity in GetIQueryable().Where(predicate).ToList())
            {
                Delete(entity);
            }
        }

        public async Task DeleteAsync(Expression<Func<TEntity, bool>> predicate)
        {
            foreach (var entity in GetIQueryable().Where(predicate).ToList())
            {
                await DeleteAsync(entity);
            }
        }

        public int Count()
        {
            return GetIQueryable().Count();
        }

        public async Task<int> CountAsync()
        {
            return await GetIQueryable().CountAsync();
        }

        public int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return GetIQueryable().Where(predicate).Count();
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await GetIQueryable().Where(predicate).CountAsync();
        }

        public long LongCount()
        {
            return GetIQueryable().LongCount();
        }

        public async Task<long> LongCountAsync()
        {
            return await GetIQueryable().LongCountAsync();
        }

        public long LongCount(Expression<Func<TEntity, bool>> predicate)
        {
            return GetIQueryable().Where(predicate).LongCount();
        }

        public async Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await GetIQueryable().Where(predicate).LongCountAsync();
        }

        #region 私有方法

        /// <summary>
        /// 检查实体是否处于跟踪状态，如果是则返回，如果没有则添加跟踪状态
        /// </summary>
        /// <param name="entity"></param>
        protected virtual void AttachIfNot(TEntity entity)
        {
            var entry = _dbContext.ChangeTracker.Entries()
                .FirstOrDefault(ent => ent.Entity == entity);

            if (entry != null)
            {
                return;
            }

            Table.Attach(entity);
        }

        protected void Save()
        {
            //调用数据库上下文保存数据
            _dbContext.SaveChanges();
        }

        protected async Task SaveAsync()
        {
            //调用数据库上下文保存数据的异步方法
            await _dbContext.SaveChangesAsync();
        }

        #endregion 私有方法
    }
}