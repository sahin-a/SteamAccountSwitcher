using SteamAccountManager.Domain.Steam.Blacklisting.Model;
using SteamAccountManager.Domain.Steam.Blacklisting.Storage;
using SteamAccountManager.Domain.Steam.Local.Logger;
using SteamAccountManager.Infrastructure.Steam.Local.Dao;
using SteamAccountManager.Infrastructure.Steam.Local.DataSource;

namespace SteamAccountManager.Infrastructure.Steam.Local.Storage;

public class BlacklistedAccountsStorage : ObjectStorage<AccountBlacklist>, IBlacklistedAccountsStorage
{
    public BlacklistedAccountsStorage(ILogger logger, IFileProvider fileProvider, string name = "blacklisted_accounts")
        : base(name, logger, new FileDataSource(directory: AppDataDirectory.Storages, fileProvider))
    {
    }
}