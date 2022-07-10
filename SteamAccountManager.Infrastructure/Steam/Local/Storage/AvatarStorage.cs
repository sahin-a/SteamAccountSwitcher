using System;
using System.IO;
using System.Threading.Tasks;
using SteamAccountManager.Application.Steam.Local.Logger;

namespace SteamAccountManager.Infrastructure.Steam.Local.Storage;

// TODO: Abstract File.Write.. away to get rid off dependencies
public class AvatarStorage : CacheStorage
{
    private readonly ILogger _logger;

    public AvatarStorage(ILogger logger) : base(dir: "Avatars")
    {
        _logger = logger;
    }

    private string GetPath(string id) => Path.Combine(Dir, id);

    public Uri? GetUri(string id)
    {
        var path = GetPath(id);

        if (!File.Exists(path) || string.IsNullOrEmpty(path))
            return null;

        return new Uri(Path.GetFullPath(path), UriKind.Absolute);
    }

    public async Task<byte[]?> GetBytesAsync(string id)
    {
        var path = GetPath(id);
        byte[] bytes = null;

        if (!File.Exists(path))
            return null;

        try
        {
            bytes = await File.ReadAllBytesAsync(path!);
        }
        catch (Exception e)
        {
            _logger.LogException("Failed to retrieve Avatar from storage", e);
        }

        return bytes;
    }

    public void Store(string id, byte[] bytes)
    {
        var path = GetPath(id);
        File.WriteAllBytesAsync(path, bytes);
        _logger.LogDebug($"New Avatar has been stored | {id}");
    }
}
