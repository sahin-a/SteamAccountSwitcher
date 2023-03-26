namespace SteamAccountManager.Domain.Common.Storage;

public interface IObjectStorage<T> where T : class
{
    public T? Get();
    public T Get(T defaultValue);
    public void Set(T value);
}