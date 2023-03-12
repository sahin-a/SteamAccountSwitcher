using SteamAccountManager.Domain.Common.CodeExtensions;

namespace SteamAccountManager.Domain.Common.EventSystem;

public class EventBus
{
    private readonly List<Subscription> _subscribers = new();

    private void RemoveDeadSubscribers()
    {
        _subscribers.RemoveAll(x => !x.handler.TryGetTarget(out _));
    }

    public void Subscribe(string subscriberKey, string eventKey, Action<object?> handler)
    {
        RemoveDeadSubscribers();
        var subscription = new Subscription(subscriberKey, eventKey, new WeakReference<Action<object?>>(handler));
        var index = _subscribers.FindIndex(x => x == subscription);
        if (index == -1)
        {
            _subscribers.Add(subscription);
            return;
        }

        _subscribers[index] = subscription;
    }

    public void Unsubscribe(string subscriberKey)
    {
        RemoveDeadSubscribers();
        _subscribers.RemoveAll(x => x.SubscriberKey == subscriberKey);
    }

    public void Notify(string eventKey, object? value)
    {
        _subscribers.FindAll(x => x.EventKey == eventKey)
            .ForEach(x =>
                x.handler.TryGetTarget(out Action<object?>? action).IfTrue(() => action?.Invoke(value))
            );
    }
}