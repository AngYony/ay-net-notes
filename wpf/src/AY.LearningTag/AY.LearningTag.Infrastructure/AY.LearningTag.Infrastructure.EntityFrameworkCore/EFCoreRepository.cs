using AY.LearningTag.Domain.Abstractions.Entities;
using AY.LearningTag.Domain.Abstractions.Repositories;
using AY.LearningTag.Domain.EFCore.Repositories;
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
    public class EFCoreRepository<TEntity, TDbContext> : IEFCoreRepository<TEntity, TDbContext>
        where TEntity : class, IEntity
        where TDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        protected readonly TDbContext dbContext;
        protected readonly DbSet<TEntity> Table;

        public EFCoreRepository(IDbContextFactory<TDbContext> factory)
        {
            dbContext = factory.CreateDbContext();
            Table = dbContext.Set<TEntity>();
        }

        /// <summary>
        /// 根查询，已启用非跟踪的标识解析
        /// <para>如果需要进一步改善复杂查询（特别是连接查询）的性能，推荐调用AsSplitQuery方法启用拆分SQL的查询</para>
        /// </summary>
        public virtual IQueryable<TEntity> Query => Table.AsNoTrackingWithIdentityResolution();
        public IQueryable<TEntity> GetIQueryable()
        {
            return Table.AsQueryable();
        }



        public virtual void AddRange(IEnumerable<TEntity> entities) => Table.AddRange(entities);

        public virtual Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default) => Table.AddRangeAsync(entities, cancellationToken);


        public void Attach(TEntity entity) => dbContext.Attach(entity);

        public void AttachRange(IEnumerable<TEntity> entities) => dbContext.AttachRange(entities);



        public int Count()
        {
            return GetIQueryable().Count();
        }

         
        public int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return GetIQueryable().Where(predicate).Count();
        }

        public async Task<int> CountAsync()
        {
            return await GetIQueryable().CountAsync();
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await GetIQueryable().Where(predicate).CountAsync();
        }


        public virtual void Delete(TEntity? entity)
        {
            if (entity is not null)
            {
                Table.Remove(entity);
            }
        }
         
        public void Delete(Expression<Func<TEntity, bool>> predicate)
        {
            foreach (var entity in GetIQueryable().Where(predicate).ToList())
            {
                Delete(entity);
            }
        }


        public virtual Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            Delete(entity);
            return Task.CompletedTask;
        }

         
        public async Task DeleteAsync(Expression<Func<TEntity, bool>> predicate)
        {
            foreach (var entity in GetIQueryable().Where(predicate).ToList())
            {
                await DeleteAsync(entity);
            }
        }




        public virtual void DeleteRange(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                Delete(entity);
            }
        }

        public virtual Task DeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            DeleteRange(entities);
            return Task.CompletedTask;
        }



        public void Detach(TEntity entity) => dbContext.Entry(entity).State = EntityState.Detached;

        public void DetachRange(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                Detach(entity);
            }
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

         
        

        public virtual TEntity Insert(TEntity entity) => Table.Add(entity).Entity;


        public async Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            var task = await Table.AddAsync(entity, cancellationToken);
            return task.Entity;
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

        /// <summary>
        /// 重置（更改跟踪器的）状态
        /// </summary>
        public void ResetDbContext() => dbContext.ChangeTracker.Clear();

       
        public TEntity Single(Expression<Func<TEntity, bool>> predicate)
        {
            return GetIQueryable().Single(predicate);
        }

        public async Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await GetIQueryable().SingleAsync(predicate);
        }




        public TEntity Update(TEntity entity) => Table.Update(entity).Entity;


        public Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            ////Update(entity);
            throw new NotImplementedException();
        }


        public virtual void UpdateRange(IEnumerable<TEntity> entities) => Table.UpdateRange(entities);

        public virtual Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            UpdateRange(entities);
            return Task.CompletedTask;
        }

        public EntityEntry<TEntity> GetEntityEntry(TEntity entity) => dbContext.Entry(entity);


        //public virtual int SaveChanges()
        //{
        //    GenerateConcurrencyStamp();
        //    return dbContext.SaveChanges();
        //}

        //public virtual Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        //{
        //    GenerateConcurrencyStamp();
        //    return dbContext.SaveChangesAsync(cancellationToken);
        //}


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

        #region 私有方法

        /// <summary>
        /// 检查实体是否处于跟踪状态，如果是则返回，如果没有则添加跟踪状态
        /// </summary>
        /// <param name="entity"></param>
        protected virtual void AttachIfNot(TEntity entity)
        {
            var entry = dbContext.ChangeTracker.Entries()
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
            dbContext.SaveChanges();
        }

        protected async Task SaveAsync()
        {
            //调用数据库上下文保存数据的异步方法
            await dbContext.SaveChangesAsync();
        }

        #endregion 私有方法

        public void Dispose() => dbContext?.Dispose();

        public ValueTask DisposeAsync() => dbContext?.DisposeAsync() ?? ValueTask.CompletedTask;


    }

    public class EFCoreRepository<TEntity, TPrimaryKey, TDbContext> :
        EFCoreRepository<TEntity, TDbContext>,
        IEFCoreRepository<TEntity, TPrimaryKey, TDbContext>
        where TEntity : class, IEntity<TPrimaryKey>
        where TPrimaryKey : IEquatable<TPrimaryKey>
        where TDbContext : DbContext
    {
        public EFCoreRepository(IDbContextFactory<TDbContext> factory) : base(factory)
        {
        }

        public virtual void Delete(TPrimaryKey key)
        {
            var entity = Find(key);
            Delete(entity);
        }

        public virtual Task DeleteAsync(TPrimaryKey key, CancellationToken cancellationToken = default)
        {
            Delete(key);
            return Task.CompletedTask;
        }
         
        
        public virtual void DeleteRange(IEnumerable<TPrimaryKey> keys)
        {
            var entities = Find(keys).ToArray();
            Table.AttachRange(entities);
            DeleteRange(entities);
        }
         

        public virtual Task DeleteRangeAsync(IEnumerable<TPrimaryKey> keys, CancellationToken cancellationToken = default)
        {
            DeleteRange(keys);
            return Task.CompletedTask;
        }

        public virtual TEntity? Find(TPrimaryKey key)
        {
            return Query.SingleOrDefault(x => x.Id.Equals(key));
        }

        public virtual Task<TEntity?> FindAsync(TPrimaryKey key, CancellationToken cancellationToken = default)
        {
            return Query.SingleOrDefaultAsync(x => x.Id.Equals(key), cancellationToken);
        }

        public virtual IQueryable<TEntity> Find(IEnumerable<TPrimaryKey> keys)
        {
            return Query.Where(x => keys.Contains(x.Id));
        }

        //todo：定义一组SaveChange的增删改查操作



    }
}
