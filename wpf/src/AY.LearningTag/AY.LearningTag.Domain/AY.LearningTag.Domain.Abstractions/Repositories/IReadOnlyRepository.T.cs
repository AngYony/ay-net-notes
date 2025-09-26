using AY.LearningTag.Domain.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AY.LearningTag.Domain.Abstractions.Repositories
{
    /// <summary>
    /// 只读仓储
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public interface IReadOnlyRepository<TEntity>
        where TEntity : IEntity
    {
        /// <summary>
        /// 获取仓库的查询根
        /// </summary>
        IQueryable<TEntity> Query { get; }
    }

    /// <summary>
    /// 只读仓储
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TKey">实体的唯一标识类型</typeparam>
    public interface IReadOnlyRepository<TEntity, TKey> : IReadOnlyRepository<TEntity>
        where TEntity : IEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// 查找实体
        /// </summary>
        /// <param name="key">主键</param>
        /// <returns>找到的实体</returns>
        TEntity? Find(TKey key);

        /// <summary>
        /// 查找实体
        /// </summary>
        /// <param name="key">主键</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>获取找到的实体的任务</returns>
        Task<TEntity?> FindAsync(TKey key, CancellationToken cancellationToken = default);

        /// <summary>
        /// 查找多个实体
        /// </summary>
        /// <param name="keys">主键集合</param>
        /// <returns>找到是实体集合</returns>
        IQueryable<TEntity?> Find(IEnumerable<TKey> keys);
    }


}
