namespace EShop.Domain.Abstractions.Commands;

/// <summary>
/// 命令存储
/// </summary>
public interface ICommandStore
{
    /// <summary>
    /// 保存命令
    /// </summary>
    /// <param name="command">命令实例</param>
    void Save(ICommand command);

    /// <summary>
    /// 保存命令
    /// </summary>
    /// <param name="command">命令实例</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>指示命令保存状态的任务</returns>
    Task SaveAsync(ICommand command, CancellationToken cancellationToken);
}

/// <summary>
/// 有返回值的命令存储
/// </summary>
/// <typeparam name="TResult">返回值类型</typeparam>
public interface ICommandStore<TResult> : ICommandStore
{
    /// <summary>
    /// 保存命令
    /// </summary>
    /// <param name="command">命令实例</param>
    /// <returns>自定义的返回值</returns>
    new TResult Save(ICommand command);

    /// <summary>
    /// 保存命令
    /// </summary>
    /// <param name="command">命令实例</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>获取自定义返回值的任务</returns>
    new Task<TResult> SaveAsync(ICommand command, CancellationToken cancellationToken);
}
