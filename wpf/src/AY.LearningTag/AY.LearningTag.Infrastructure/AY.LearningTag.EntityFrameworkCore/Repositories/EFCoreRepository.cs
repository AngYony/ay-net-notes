using AY.LearningTag.Domain.Abstractions.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AY.LearningTag.EntityFrameworkCore.Repositories
{
    public class EFCoreRepository<TEntity, TKey, TDbContext> : EFCoreRepository<TEntity, TDbContext>, IEFCoreRepository<TEntity, TKey, TDbContext>
        where TEntity : class, IEntity<TKey>
        where TKey : IEquatable<TKey>
        where TDbContext : DbContext
    {
        public EFCoreRepository(IDbContextFactory<TDbContext> factory) : base(factory)
        {
        }

        public virtual void Delete(TKey key)
        {
            var entity = Find(key);
            Delete(entity);
        }

        public virtual Task DeleteAsync(TKey key, CancellationToken cancellationToken = default)
        {
            Delete(key);
            return Task.CompletedTask;
        }

        public virtual void DeleteRange(IEnumerable<TKey> keys)
        {
            var entities = Find(keys).ToArray();
            dbSet.AttachRange(entities);
            DeleteRange(entities);
        }

        public virtual Task DeleteRangeAsync(IEnumerable<TKey> keys, CancellationToken cancellationToken = default)
        {
            DeleteRange(keys);
            return Task.CompletedTask;
        }

        public virtual TEntity? Find(TKey key)
        {
            return Query.SingleOrDefault(x => x.Id.Equals(key));
        }

        public virtual Task<TEntity?> FindAsync(TKey key, CancellationToken cancellationToken = default)
        {
            return Query.SingleOrDefaultAsync(x => x.Id.Equals(key), cancellationToken);
        }

        public virtual IQueryable<TEntity> Find(IEnumerable<TKey> keys)
        {
            return Query.Where(x => keys.Contains(x.Id));
        }
    }

    public class EFCoreRepository<TEntity, TDbContext> : IEFCoreRepository<TEntity, TDbContext>
        where TEntity : class, IEntity
        where TDbContext : DbContext
    {
        protected readonly TDbContext dbContext;
        protected readonly DbSet<TEntity> dbSet;

        /// <summary>
        /// 根查询，已启用非跟踪的标识解析
        /// <para>如果需要进一步改善复杂查询（特别是连接查询）的性能，推荐调用AsSplitQuery方法启用拆分SQL的查询</para>
        /// </summary>
        public virtual IQueryable<TEntity> Query => dbSet.AsNoTrackingWithIdentityResolution();

        public EFCoreRepository(IDbContextFactory<TDbContext> factory)
        {
            dbContext = factory.CreateDbContext();
            dbSet = dbContext.Set<TEntity>();
        }

        public virtual void Add(TEntity entity) => dbSet.Add(entity);

        public virtual Task AddAsync(TEntity entity, CancellationToken cancellationToken = default) => dbSet.AddAsync(entity, cancellationToken).AsTask();

        public virtual void AddRange(IEnumerable<TEntity> entities) => dbSet.AddRange(entities);

        public virtual Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default) => dbSet.AddRangeAsync(entities, cancellationToken);

        public virtual void Delete(TEntity? entity)
        {
            if (entity is not null)
            {
                dbSet.Remove(entity);
            }
        }

        public virtual Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            Delete(entity);
            return Task.CompletedTask;
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

        public virtual void Update(TEntity entity) => dbSet.Update(entity);

        public virtual Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            Update(entity);
            return Task.CompletedTask;
        }

        public virtual void UpdateRange(IEnumerable<TEntity> entities) => dbSet.UpdateRange(entities);

        public virtual Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            UpdateRange(entities);
            return Task.CompletedTask;
        }

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

        public EntityEntry<TEntity> GetEntityEntry(TEntity entity) => dbContext.Entry(entity);

        /// <summary>
        /// 重置（更改跟踪器的）状态
        /// </summary>
        public void ResetDbContext() => dbContext.ChangeTracker.Clear();

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
    }
}
