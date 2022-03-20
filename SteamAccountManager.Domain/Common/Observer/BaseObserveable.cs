namespace SteamAccountManager.Domain.Common.Observer
{
    internal abstract class BaseObserveable<T> : IObserveable<T>
    {
        private List<Action<T?>> _observers { get; set; } = new();

        public void Notify(T value)
        {
            _observers.ForEach(o => o(value));
        }

        public IDisposable Subscribe(Action<T> observer)
        {
            _observers.Add(observer);
            return new Unsubscriber<T>(_observers, observer);
        }
    }
}
