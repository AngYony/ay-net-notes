using EShop.Domain.Abstractions.Entities;

namespace EShop.Domain.Abstractions.Repositories;

/// <summary>
/// 支持批处理的仓储
/// </summary>
/// <typeparam name="TResult">处理结果的类型</typeparam>
/// <typeparam name="TVariableRepository">基础仓库类型</typeparam>
/// <typeparam name="TEntity">实体类型</typeparam>
public interface IBulkOperableVariableRepository<TResult, TVariableRepository, TEntity>
    where TEntity : IEntity
    where TVariableRepository : IVariableRepository<TEntity>
{
    /// <summary>
    /// 保存变更
    /// </summary>
    /// <returns>返回值</returns>
    TResult SaveChanges();

    /// <summary>
    /// 保存变更
    /// </summary>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>获取返回值的任务</returns>
    Task<TResult> SaveChangesAsync(CancellationToken cancellationToken = default);
}

/// <summary>
/// 支持批处理的仓储
/// </summary>
/// <typeparam name="TVariableRepository">基础仓库类型</typeparam>
/// <typeparam name="TEntity">实体类型</typeparam>
public interface IBulkOperableVariableRepository<TVariableRepository, TEntity>
    where TEntity : IEntity
    where TVariableRepository : IVariableRepository<TEntity>
{
    /// <summary>
    /// 保存变更
    /// </summary>
    void SaveChanges();

    /// <summary>
    /// 保存变更
    /// </summary>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>指示保存状态的任务</returns>
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}

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

/// <summary>
/// 可变仓储
/// </summary>
/// <typeparam name="TEntity">实体类型</typeparam>
public interface IVariableRepository<TEntity>
    where TEntity : IEntity
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

/// <summary>
/// 可读写仓储
/// </summary>
/// <typeparam name="TEntity">实体类型</typeparam>
public interface IRepository<TEntity> : IVariableRepository<TEntity>, IReadOnlyRepository<TEntity>
    where TEntity : IEntity
{
}

/// <summary>
/// 可读写仓储
/// </summary>
/// <typeparam name="TEntity">实体类型</typeparam>
/// <typeparam name="TKey">实体的唯一标识类型</typeparam>
public interface IRepository<TEntity, TKey> : IRepository<TEntity>, IVariableRepository<TEntity, TKey>, IReadOnlyRepository<TEntity, TKey>
    where TEntity : IEntity<TKey>
    where TKey : IEquatable<TKey>
{
}
