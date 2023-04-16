namespace SteamAccountManager.Domain.Steam.Configuration.Model;

public class NotificationConfig
{
    public bool IsAllowedToSendNotification { get; set; }

    public NotificationConfig(bool isAllowedToSendNotification)
    {
        IsAllowedToSendNotification = isAllowedToSendNotification;
    }
}