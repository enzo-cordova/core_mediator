namespace Genzai.Core.Integration;

/// <summary>
/// BaseMessageEventHandler
/// </summary>
/// <typeparam name="T"></typeparam>
public interface BaseMessageEventHandler<in T> : BaseMessageEventHandler
    where T : BaseMessage
{
    /// <summary>
    /// Handle
    /// </summary>
    /// <param name="event"></param>
    /// <returns></returns>
    Task Handle(T @event);
}

/// <summary>
/// BaseMessageEventHandler
/// </summary>
public interface BaseMessageEventHandler
{
}