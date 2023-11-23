namespace EShop.Domain.Abstractions.Events;

/// <summary>
/// 事件处理器
/// </summary>
/// <typeparam name="TEvent">事件类型</typeparam>
public interface IEventHandler<in TEvent>
    where TEvent : IEvent
{
    /// <summary>
    /// 处理事件
    /// </summary>
    /// <param name="event">事件实例</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>指示事件处理状态的任务</returns>
    Task Handle(TEvent @event, CancellationToken cancellationToken);
}
