using AY.LearningTag.Domain.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AY.LearningTag.Domain.Abstractions.Repositories
{
    //注意：这里面虽然名称叫SaveChanges，但它并不局限于EF Core的上下文保存操作，任何支持批处理的仓储都可以实现这个接口

    /// <summary>
    /// 支持批处理的仓储
    /// </summary>
    /// <typeparam name="TVariableRepository">基础仓库类型</typeparam>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public interface IBulkOperableVariableRepository
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
    public interface IBulkOperableVariableRepository<TResult>
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
