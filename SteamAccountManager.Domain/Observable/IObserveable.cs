namespace SteamAccountManager.Domain.Observable
{
    public interface IObserveable<T>
    {
        public IDisposable Subscribe(Action<T?> observeable);
        public void Notify(T? value);
    }
}
