﻿namespace EShop.Domain.Abstractions.Commands;

/// <summary>
/// 无返回值的命令总线（每种命令只会分发给一种命令处理器）
/// </summary>
/// <typeparam name="TCommand">命令类型</typeparam>
public interface ICommandBus<in TCommand>
    where TCommand : ICommand
{
    /// <summary>
    /// 发送命令
    /// </summary>
    /// <param name="command">命令实例</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>指示命令发送或处理状态的任务</returns>
    Task SendCommandAsync(TCommand command, CancellationToken cancellationToken);
}

/// <summary>
/// 有返回值的命令总线（每种命令只会分发给一种命令处理器）
/// </summary>
/// <typeparam name="TCommand">命令类型</typeparam>
/// <typeparam name="TResult">返回值类型</typeparam>
public interface ICommandBus<in TCommand, TResult> : ICommandBus<TCommand>
    where TCommand : ICommand<TResult>
{
    /// <summary>
    /// 发送命令
    /// </summary>
    /// <param name="command">命令实例</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>表示命令返回值的任务</returns>
    new Task<TResult> SendCommandAsync(TCommand command, CancellationToken cancellationToken);
}