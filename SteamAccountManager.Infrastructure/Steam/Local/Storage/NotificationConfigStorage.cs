using SteamAccountManager.Domain.Steam.Configuration.Model;
using SteamAccountManager.Domain.Steam.Local.Logger;
using SteamAccountManager.Domain.Steam.Storage;
using SteamAccountManager.Infrastructure.Steam.Local.Dao;
using SteamAccountManager.Infrastructure.Steam.Local.DataSource;

namespace SteamAccountManager.Infrastructure.Steam.Local.Storage;

public class NotificationConfigStorage : ObjectStorage<NotificationConfig>, INotificationConfigStorage
{
    public NotificationConfigStorage(ILogger logger, IFileProvider fileProvider, string name = "notification_config") :
        base(name, logger, new FileDataSource(directory: AppDataDirectory.Configurations, fileProvider))
    {
    }

    protected override NotificationConfig GetDefaultValue() => new(isAllowedToSendNotification: true);
}