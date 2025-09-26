using AY.LearningTag.Domain.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AY.LearningTag.Domain.Abstractions.Repositories
{
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
}
