using System.IO;
using System.Threading.Tasks;
using SteamAccountManager.Infrastructure.Steam.Local.Dao;

namespace SteamAccountManager.Infrastructure.Steam.Local.DataSource;

public class FileDataSource : IKeyValueDataSource
{
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
        return await _fileProvider.WriteAllText(path: GetFilePath(key), content: value);
    }

    public async Task<string?> Load(string key)
    {
        return await _fileProvider.ReadAllText(path: GetFilePath(key));
    }
}