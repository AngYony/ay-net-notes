namespace EShop.Domain.Abstractions.Messages;

/// <summary>
/// 消息，命令和事件的通用基础
/// </summary>
public interface IMessage
{
    /// <summary>
    /// 消息的唯一标识
    /// </summary>
    string Id { get; }

    /// <summary>
    /// 消息的发生时间
    /// </summary>
    DateTimeOffset Timestamp { get; }
}
