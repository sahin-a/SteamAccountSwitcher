using SteamAccountManager.Domain.Steam.Configuration.Model;
using SteamAccountManager.Domain.Steam.Local.Logger;
using SteamAccountManager.Domain.Steam.Storage;
using SteamAccountManager.Infrastructure.Steam.Local.Dao;
using SteamAccountManager.Infrastructure.Steam.Local.DataSource;

namespace SteamAccountManager.Infrastructure.Steam.Local.Storage;

public class RichPresenceConfigStorage : ObjectStorage<RichPresenceConfig>, IRichPresenceConfigStorage
{
    public RichPresenceConfigStorage(ILogger logger, IFileProvider fileProvider,
        string name = "discord_rich_presence") :
        base(name, logger, new FileDataSource(directory: AppDataDirectory.Configurations, fileProvider))
    {
    }
}