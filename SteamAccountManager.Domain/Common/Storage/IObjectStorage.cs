namespace SteamAccountManager.Domain.Common.Storage;

public interface IObjectStorage<T> where T : class
{
    public Task<T?> Get();
    public Task<T> Get(T defaultValue);
    public Task Set(T value);
}