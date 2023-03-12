namespace SteamAccountManager.Domain.Common.Storage
{
    public interface IStorage<T> where T : class
    {
        public T Get();
        public void Set(T value);
    }

    internal interface IAsyncStorage<T> where T : class
    {
        public Task<T> Get();
        public void Set(T value);
    }
}