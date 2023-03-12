namespace SteamAccountManager.Domain.Common.EventSystem;

class Subscription
{
    public string SubscriberKey { get; set; }
    public string EventKey { get; set; }
    public WeakReference<Action<object?>> handler { get; set; }

    public Subscription(string subscriberKey, string eventKey, WeakReference<Action<object?>> handler)
    {
        SubscriberKey = subscriberKey;
        EventKey = eventKey;
        this.handler = handler;
    }

    public override bool Equals(object obj)
    {
        if (obj is null || obj is not Subscription)
            return false;

        var sub = obj as Subscription;

        return SubscriberKey == sub.SubscriberKey && EventKey == sub.EventKey;
    }
}