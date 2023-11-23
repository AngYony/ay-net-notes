namespace EShop.Domain.Abstractions.Events;

/// <summary>
/// 事件总线
/// </summary>
public interface IEventBus
{
    /// <summary>
    /// 发布事件
    /// </summary>
    /// <param name="event">事件实例</param>
    void PublishEvent(IEvent @event);

    /// <summary>
    /// 发布事件
    /// </summary>
    /// <param name="event">事件实例</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>指示事件发布状态的任务</returns>
    Task PublishEventAsync(IEvent @event, CancellationToken cancellationToken);
}

/// <summary>
/// 有返回值的事件总线
/// </summary>
/// <typeparam name="TResult">返回值的类型</typeparam>
public interface IEventBus<TResult> : IEventBus
{
    /// <summary>
    /// 发布事件
    /// </summary>
    /// <param name="event">事件实例</param>
    /// <returns>返回值</returns>
    new TResult? PublishEvent(IEvent @event);

    /// <summary>
    /// 发布事件
    /// </summary>
    /// <param name="event">事件实例</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>获取返回值的任务</returns>
    new Task<TResult?> PublishEventAsync(IEvent @event, CancellationToken cancellationToken);
}
