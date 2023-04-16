using System.IO;
using System.Threading.Tasks;
using SteamAccountManager.Infrastructure.Steam.Local.Dao;

namespace SteamAccountManager.Infrastructure.Steam.Local.DataSource;

public class FileDataSource : IKeyValueDataSource
{
    private readonly object _lock = new();
    private readonly string _directory;
    private readonly IFileProvider _fileProvider;

    public FileDataSource(string directory, IFileProvider fileProvider)
    {
        _directory = directory;
        _fileProvider = fileProvider;
    }

    private string GetFilePath(string key) => Path.Combine(_directory, $"{key}.json");

    public async Task<bool> Store(string key, string value)
    {
        Task<bool>? task;
        lock (_lock)
        {
            task = _fileProvider.WriteAllText(path: GetFilePath(key), content: value);
        }

        return await task;
    }

    public async Task<string?> Load(string key)
    {
        Task<string?>? task;
        lock (_lock)
        {
            task = _fileProvider.ReadAllText(path: GetFilePath(key));
        }

        return await task;
    }
}