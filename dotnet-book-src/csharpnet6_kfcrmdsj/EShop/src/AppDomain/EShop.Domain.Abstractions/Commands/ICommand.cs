using EShop.Domain.Abstractions.Messages;

namespace EShop.Domain.Abstractions.Commands;

/// <summary>
/// 有返回值的命令接口
/// </summary>
/// <typeparam name="TResult">返回值类型</typeparam>
public interface ICommand<out TResult> : ICommand
{
}

/// <summary>
/// 无返回值的命令接口
/// </summary>
public interface ICommand : IMessage
{
}
