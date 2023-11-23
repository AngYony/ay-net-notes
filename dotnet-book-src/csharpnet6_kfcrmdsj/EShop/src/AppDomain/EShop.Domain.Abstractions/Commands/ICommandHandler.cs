namespace EShop.Domain.Abstractions.Commands;

/// <summary>
/// 无返回值的命令处理器
/// </summary>
/// <typeparam name="TCommand">命令类型</typeparam>
public interface ICommandHandler<in TCommand>
    where TCommand : ICommand
{
    /// <summary>
    /// 处理命令
    /// </summary>
    /// <param name="command">命令实例</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>指示命令处理状态的任务</returns>
    Task Handle(TCommand command, CancellationToken cancellationToken);
}

/// <summary>
/// 有返回值的命令处理器
/// </summary>
/// <typeparam name="TCommand">命令类型</typeparam>
/// <typeparam name="TResult">返回值类型</typeparam>
public interface ICommandHandler<in TCommand, TResult> : ICommandHandler<TCommand>
    where TCommand : ICommand<TResult>
{
    /// <summary>
    /// 处理命令
    /// </summary>
    /// <param name="command">命令实例</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>获取命令返回值的任务</returns>
    new Task<TResult> Handle(TCommand command, CancellationToken cancellationToken);
}
