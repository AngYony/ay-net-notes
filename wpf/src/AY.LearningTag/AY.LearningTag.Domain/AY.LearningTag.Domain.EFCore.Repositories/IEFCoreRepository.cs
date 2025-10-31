using AY.LearningTag.Domain.Abstractions.Entities;
using AY.LearningTag.Domain.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * 通过EFCore实现的仓储接口
 */

//todo：验证将泛型的DbContext移除的情况
namespace AY.LearningTag.Domain.EFCore.Repositories
{
    /// <summary>
    /// EFCore仓储接口
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IEFCoreRepository<TEntity, TDbContext> 
        : IRepository<TEntity>, IDisposable, IAsyncDisposable

        where TEntity :class, IEntity
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
    /// 根据主键的EFCore仓储接口
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TPrimaryKey"></typeparam>
    public interface IEFCoreRepository<TEntity, TPrimaryKey, TDbContext> :
        IEFCoreRepository<TEntity, TDbContext>,
        IRepository<TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
        where TPrimaryKey : IEquatable<TPrimaryKey>
        where TDbContext : DbContext
    {
    }
}
