using System.Threading.Tasks;

namespace SteamAccountManager.Infrastructure.Steam.Local.Dao;

public interface IFileProvider
{
    public Task<string?> ReadAllText(string path);
    public Task<bool> WriteAllText(string path, string content);
}