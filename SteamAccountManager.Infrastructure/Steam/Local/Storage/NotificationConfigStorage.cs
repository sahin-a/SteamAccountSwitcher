using SteamAccountManager.Domain.Steam.Configuration.Model;
using SteamAccountManager.Domain.Steam.Local.Logger;
using SteamAccountManager.Domain.Steam.Storage;

namespace SteamAccountManager.Infrastructure.Steam.Local.Storage;

public class NotificationConfigStorage : ObjectStorage<NotificationConfig>, INotificationConfigStorage
{
    public NotificationConfigStorage(ILogger logger, string fileName = "notification_config") : base(fileName, logger)
    {
    }

    protected override NotificationConfig GetDefaultValue()
    {
        return new NotificationConfig(isAllowedToSendNotification: true);
    }
}