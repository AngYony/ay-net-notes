using AY.LearningTag.Domain.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AY.LearningTag.Domain.Abstractions.Repositories
{
    /// <summary>
    /// 包含增删改操作的仓储
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IVariableRepository<TEntity> where TEntity : IEntity
    {
        #region Insert

        /// <summary>
        /// 添加一个新实体信息
        /// </summary>
        /// <param name="entity">被添加的实体</param>
        TEntity Insert(TEntity entity);

        /// <summary>
        /// 添加一个新实体信息
        /// </summary>
        /// <param name="entity">实体实例</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>指示添加状态的任务</returns>
        Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken = default);

        #endregion

        #region Update

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity">实体实例</param>
        TEntity Update(TEntity entity);

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity">实体实例</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>指示更新状态的任务</returns>
        Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

        #endregion

        #region Delete

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity">实体实例</param>
        void Delete(TEntity entity);

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity">实体实例</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>指示删除状态的任务</returns>
        Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        ///按传入的条件可删除多个实体。
        ///注意:所有符合给定条件的实体都将被检索和删除。
        ///如果条件比较多，待删除的实体也比较多，这可能会导致主要的性能问题。
        /// </summary>
        /// <param name="predicate">筛选实体的条件</param>
        void Delete(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        ///按传入的条件可删除多个实体。
        ///注意:所有符合给定条件的实体都将被检索和删除。
        ///如果条件比较多，待删除的实体也比较多，这可能会导致主要的性能问题。
        /// </summary>
        /// <param name="predicate">筛选实体的条件</param>
        Task DeleteAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 添加多个实体
        /// </summary>
        /// <param name="entities">实体实例集合</param>
        void AddRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// 添加多个实体
        /// </summary>
        /// <param name="entities">实体实例集合</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>指示添加状态的任务</returns>
        Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

        /// <summary>
        /// 更新多个实体
        /// </summary>
        /// <param name="entities">实体实例集合</param>
        void UpdateRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// 更新多个实体
        /// </summary>
        /// <param name="entities">实体实例集合</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>指示更新状态的任务</returns>
        Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

        /// <summary>
        /// 删除多个实体
        /// </summary>
        /// <param name="entities">实体实例集合</param>
        void DeleteRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// 删除多个实体
        /// </summary>
        /// <param name="entities">实体实例集合</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>指示删除状态的任务</returns>
        Task DeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

        #endregion

    }

    /// <summary>
    /// 根据主键包含删除操作的仓储
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TPrimaryKey">实体的唯一标识类型</typeparam>
    public interface IVariableRepository<TEntity, TPrimaryKey> : IVariableRepository<TEntity>
        where TEntity : IEntity<TPrimaryKey> 
        where TPrimaryKey : IEquatable<TPrimaryKey>
    {
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="key">主键</param>
        void Delete(TPrimaryKey key);

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="key">主键</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>指示删除状态的任务</returns>
        Task DeleteAsync(TPrimaryKey key, CancellationToken cancellationToken = default);

        /// <summary>
        /// 删除多个实体
        /// </summary>
        /// <param name="keys">主键集合</param>
        void DeleteRange(IEnumerable<TPrimaryKey> keys);

        /// <summary>
        /// 删除多个实体
        /// </summary>
        /// <param name="keys">主键集合</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>指示删除状态的任务</returns>
        Task DeleteRangeAsync(IEnumerable<TPrimaryKey> keys, CancellationToken cancellationToken = default);
    }

}
