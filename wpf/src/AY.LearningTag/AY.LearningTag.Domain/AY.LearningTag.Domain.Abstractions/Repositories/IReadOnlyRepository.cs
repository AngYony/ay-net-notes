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
    /// 只读仓库，只读取数据
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public interface IReadOnlyRepository<TEntity> where TEntity : IEntity
    {
        /// <summary>
        /// 用于获取用于从整个表中检索实体的IQueryable
        /// </summary>
        IQueryable<TEntity> Query { get; }

        /// <summary>
        /// 用于获取用于从整个表中检索实体的IQueryable
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> GetIQueryable();


        /// <summary>
        /// 用于获取所有实体。
        /// </summary>
        /// <returns>所有实体列表</returns>
        List<TEntity> GetAllList();

        /// <summary>
        /// 用于获取所有实体的异步实现
        /// </summary>
        /// <returns>所有实体列表</returns>
        Task<List<TEntity>> GetAllListAsync();

        /// <summary>
        /// 用于获取传入本方法的所有实体 <paramref name="predicate"/>.
        /// </summary>
        /// <param name="predicate">筛选实体的条件</param>
        /// <returns>所有实体列表</returns>
        List<TEntity> GetAllList(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 用于获取传入本方法的所有实体<paramref name="predicate"/>.
        /// </summary>
        /// <param name="predicate">筛选实体的条件</param>
        /// <returns>所有实体列表</returns>
        Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 通过传入的筛选条件来获取实体信息 通过传入的筛选条件来获取实体信息
        /// 如果查询不到返回值则会引发异常
        /// </summary>
        /// <param name="predicate">Entity</param>
        TEntity Single(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 通过传入的筛选条件来获取实体信息
        /// 如果查询不到返回值则会引发异常
        /// </summary>
        /// <param name="predicate">Entity</param>
        Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 通过传入的筛选条件查询实体信息，如果没有找到，则返回null。
        /// </summary>
        /// <param name="predicate">筛选条件</param>
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 通过传入的筛选条件查询实体信息，如果没有找到，则返回null。
        /// </summary>
        /// <param name="predicate">筛选条件</param>
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);


        #region 总和计算

        /// <summary>
        /// 获取此仓储中所有实体的总和。
        /// </summary>
        /// <returns>实体的总数</returns>
        int Count();

        /// <summary>
        /// 获取此仓储中所有实体的总和。
        /// </summary>
        /// <returns>实体的总数</returns>
        Task<int> CountAsync();

        /// <summary>
        /// 支持条件筛选 <paramref name="predicate"/>计算仓储中的实体总和
        /// </summary>
        /// <param name="predicate">实体的总数</param>
        /// <returns>实体的总数</returns>
        int Count(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        ///支持条件筛选 <paramref name="predicate"/>计算仓储中的实体总和
        /// </summary>
        /// <param name="predicate">实体的总数</param>
        /// <returns>实体的总数</returns>
        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 获取此存储库中所有实体的总和(如果预期返回值大于了Int.MaxValue值，则推荐该方法)，简单来说就是返回值为long类型
        /// <see cref="int.MaxValue"/>.
        /// </summary>
        /// <returns>实体的总数</returns>
        long LongCount();

        /// <summary>
        /// 获取此存储库中所有实体的总和(如果预期返回值大于了Int.MaxValue值，则推荐该方法)，简单来说就是返回值为long类型<see cref="int.MaxValue"/>.
        /// </summary>
        /// <returns>实体的总数</returns>
        Task<long> LongCountAsync();

        /// <summary>
        ///支持条件筛选获取此存储库中所有实体的总和(如果预期返回值大于了Int.MaxValue值，则推荐该方法)，简单来说就是返回值为long类型
        ///<see cref="int.MaxValue"/>).
        /// </summary>
        /// <param name="predicate">实体的总数</param>
        /// <returns>实体的总数</returns>
        long LongCount(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 支持条件筛选<paramref name="predicate"/>获取此存储库中所有实体的总和(如果预期返回值大于了Int.MaxValue值，则推荐该方法)，简单来说就是返回值为long类型
        ///<see cref="int.MaxValue"/>).
        /// </summary>
        /// <param name="predicate">实体的总数</param>
        /// <returns>实体的总数</returns>
        Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate);

        #endregion 总和计算


    }


    /// <summary>
    /// 根据主键读取实体的只读仓库
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TPrimaryKey">实体的唯一标识类型</typeparam>
    public interface IReadOnlyRepository<TEntity, TPrimaryKey> : IReadOnlyRepository<TEntity>
        where TEntity : IEntity<TPrimaryKey>
        where TPrimaryKey : IEquatable<TPrimaryKey>
    {
        /// <summary>
        /// 查找实体
        /// </summary>
        /// <param name="key">主键</param>
        /// <returns>找到的实体</returns>
        TEntity? Find(TPrimaryKey key);

        /// <summary>
        /// 查找实体
        /// </summary>
        /// <param name="key">主键</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>获取找到的实体的任务</returns>
        Task<TEntity?> FindAsync(TPrimaryKey key, CancellationToken cancellationToken = default);

        /// <summary>
        /// 查找多个实体
        /// </summary>
        /// <param name="keys">主键集合</param>
        /// <returns>找到是实体集合</returns>
        IQueryable<TEntity?> Find(IEnumerable<TPrimaryKey> keys);
    }

}
