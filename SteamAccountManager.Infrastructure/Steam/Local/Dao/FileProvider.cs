using System;
using System.IO;
using System.Threading.Tasks;
using SteamAccountManager.Domain.Steam.Local.Logger;

namespace SteamAccountManager.Infrastructure.Steam.Local.Dao;

public class FileProvider : IFileProvider
{
    private readonly Lazy<ILogger> _logger;

    public FileProvider(Lazy<ILogger> logger)
    {
        _logger = logger;
    }

    public async Task<string?> ReadAllText(string path)
    {
        try
        {
            return await File.ReadAllTextAsync(path: path);
        }
        catch (Exception e)
        {
            _logger.Value.LogWarning($"Failed to read from file at: {path}", e);
        }

        return null;
    }

    public async Task<bool> WriteAllText(string path, string content, bool append)
    {
        var directoryPath = Path.GetDirectoryName(path);

        if (string.IsNullOrEmpty(directoryPath))
            throw new Exception($"Directory invalid: {directoryPath}");

        Directory.CreateDirectory(directoryPath);

        try
        {
            await using var sw = new StreamWriter(path: path, append: append);
            await sw.WriteLineAsync(content);

            return true;
        }
        catch (Exception e)
        {
            _logger.Value.LogWarning($"Failed to write to file at: {path}", e);
        }

        return false;
    }
}