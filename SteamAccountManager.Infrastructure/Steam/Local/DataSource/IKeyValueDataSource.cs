using System.Threading.Tasks;

namespace SteamAccountManager.Infrastructure.Steam.Local.DataSource;

public interface IKeyValueDataSource
{
    public Task<bool> Store(string key, string value);
    public Task<string?> Load(string key);
}