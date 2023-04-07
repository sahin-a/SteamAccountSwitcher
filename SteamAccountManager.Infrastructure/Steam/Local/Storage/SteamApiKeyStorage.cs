using SteamAccountManager.Domain.Steam.Local.Logger;
using SteamAccountManager.Domain.Steam.Storage;
using SteamAccountManager.Infrastructure.Steam.Local.Dao;
using SteamAccountManager.Infrastructure.Steam.Local.DataSource;

namespace SteamAccountManager.Infrastructure.Steam.Local.Storage
{
    public class SteamApiKeyStorage : ObjectStorage<string>, ISteamApiKeyStorage
    {
        public SteamApiKeyStorage(ILogger logger, IFileProvider fileProvider, string name = "api_key") : base(name,
            logger, new FileDataSource(directory: AppDataDirectory.Storages, fileProvider))
        {
        }
    }
}