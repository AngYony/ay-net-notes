using AY.LearningTag.Domain.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AY.LearningTag.Domain.Abstractions.Repositories
{
    /// <summary>
    /// 可变仓储
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public interface IVariableRepository<TEntity> where TEntity : IEntity
    {
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="entity">实体实例</param>
        void Add(TEntity entity);

        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="entity">实体实例</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>指示添加状态的任务</returns>
        Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity">实体实例</param>
        void Update(TEntity entity);

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity">实体实例</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>指示更新状态的任务</returns>
        Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

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
    }


    /// <summary>
    /// 可变仓储
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TKey">实体的唯一标识类型</typeparam>
    public interface IVariableRepository<TEntity, TKey> : IVariableRepository<TEntity>
        where TEntity : IEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="key">主键</param>
        void Delete(TKey key);

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="key">主键</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>指示删除状态的任务</returns>
        Task DeleteAsync(TKey key, CancellationToken cancellationToken = default);

        /// <summary>
        /// 删除多个实体
        /// </summary>
        /// <param name="keys">主键集合</param>
        void DeleteRange(IEnumerable<TKey> keys);

        /// <summary>
        /// 删除多个实体
        /// </summary>
        /// <param name="keys">主键集合</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>指示删除状态的任务</returns>
        Task DeleteRangeAsync(IEnumerable<TKey> keys, CancellationToken cancellationToken = default);
    }

}
