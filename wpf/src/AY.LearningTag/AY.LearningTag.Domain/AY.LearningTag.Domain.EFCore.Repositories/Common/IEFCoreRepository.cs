using AY.LearningTag.Domain.Abstractions.Entities;
using AY.LearningTag.Domain.Abstractions.Repositories;
using System.Linq.Expressions;
//using Microsoft.EntityFrameworkCore.ChangeTracking;

/*
 * 通过EFCore实现的仓储接口
 */

namespace AY.LearningTag.Domain.EFCore.Repositories.Common
{
    /// <summary>
    /// EFCore仓储接口
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IEFCoreRepository<TEntity>
        : IRepository<TEntity>,
        IBulkOperableVariableRepository<int>,
        IDisposable, IAsyncDisposable

        where TEntity : class, IEntity
    {
        #region Insert
        /// <summary>
        /// 自动保存添加单个实体
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="autoSave"></param>
        /// <returns></returns>
        TEntity Insert(TEntity entity, bool autoSave = false);
        /// <summary>
        /// 自动保存添加单个实体
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="autoSave"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TEntity> InsertAsync(TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default);
        /// <summary>
        /// 自动保存同时添加多个实体
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="autoSave"></param>
        void InsertMany(IEnumerable<TEntity> entities, bool autoSave = false);

        /// <summary>
        /// 自动保存同时添加多个实体
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="autoSave"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task InsertManyAsync(IEnumerable<TEntity> entities, bool autoSave = false, CancellationToken cancellationToken = default);
        #endregion

        #region Delete

        void Delete(TEntity? entity, bool autoSave = false);
        void Delete(Expression<Func<TEntity, bool>> predicate, bool autoSave = false);
        void DeleteMany(IEnumerable<TEntity> entities, bool autoSave = false);

        #endregion

        #region Update
        TEntity Update(TEntity entity, bool autoSave = false);
        void UpdateMany(IEnumerable<TEntity> entities, bool autoSave = false);
        #endregion


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

        ///// <summary>
        ///// 获取实体记录
        ///// </summary>
        ///// <param name="entity">实体实例</param>
        ///// <returns></returns>
        //EntityEntry<TEntity> GetEntityEntry(TEntity entity);

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
    public interface IEFCoreRepository<TEntity, TPrimaryKey> :
        IEFCoreRepository<TEntity>,
        IRepository<TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
        where TPrimaryKey : IEquatable<TPrimaryKey>
    {
    }
}