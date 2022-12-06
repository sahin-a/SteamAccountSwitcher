using System.IO;

namespace SteamAccountManager.Infrastructure.Steam.Local.Storage;

public abstract class CacheStorage
{
    protected string Dir { get; private set; }
    
    protected CacheStorage(string dir)
    {
        Dir = Path.Combine("Cache", dir);
        Directory.CreateDirectory(Dir);
    }
}