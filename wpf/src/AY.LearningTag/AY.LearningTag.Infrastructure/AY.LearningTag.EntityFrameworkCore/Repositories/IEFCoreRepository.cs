using AY.LearningTag.Domain.Abstractions.Entities;
using AY.LearningTag.Domain.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AY.LearningTag.EntityFrameworkCore.Repositories
{
    /// <summary>
    /// 无主键实体的EF Core仓储
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TDbContext">EF Core上下文类型</typeparam>
    public interface IEFCoreRepository<TEntity, TDbContext> : IReadOnlyRepository<TEntity>, IVariableRepository<TEntity>, IBulkOperableVariableRepository<int, IEFCoreRepository<TEntity, TDbContext>, TEntity>, IDisposable, IAsyncDisposable
        where TEntity : class, IEntity
        where TDbContext : DbContext
    {
        /// <summary>
        /// 开始跟踪实体变更
        /// </summary>
        /// <param name="entity">实体实例</param>
        void Attach(TEntity entity);

        /// <summary>
        /// 开始跟踪多个实体变更
        /// </summary>
        /// <param name="entities">实体集合</param>
        void AttachRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// 停止跟踪实体变更
        /// </summary>
        /// <param name="entity">实体实例</param>
        void Detach(TEntity entity);

        /// <summary>
        /// 停止跟踪多个实体变更
        /// </summary>
        /// <param name="entities">实体集合</param>
        void DetachRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// 获取实体记录
        /// </summary>
        /// <param name="entity">实体实例</param>
        /// <returns></returns>
        EntityEntry<TEntity> GetEntityEntry(TEntity entity);

        /// <summary>
        /// 重置实体变更跟踪器的状态
        /// </summary>
        void ResetDbContext();
    }

    /// <summary>
    /// 有主键实体的EF Core仓储
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TKey">主键类型</typeparam>
    /// <typeparam name="TDbContext">EF Core上下文类型</typeparam>
    public interface IEFCoreRepository<TEntity, TKey, TDbContext> : IEFCoreRepository<TEntity, TDbContext>, IReadOnlyRepository<TEntity, TKey>, IVariableRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
        where TKey : IEquatable<TKey>
        where TDbContext : DbContext
    { }
}
