namespace SteamAccountManager.Domain.Common.Storage;

public interface IObjectStorage<T> where T : class
{
    public T? Get();
    public void Set(T value);
}