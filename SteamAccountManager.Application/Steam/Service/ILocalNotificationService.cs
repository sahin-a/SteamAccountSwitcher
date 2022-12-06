namespace SteamAccountManager.Application.Steam.Service
{
    public class Notification
    {
        public Notification(string? title = null, string? message = null, Uri? logo = null)
        {
            Title = title;
            Message = message;
            Logo = logo;
        }

        public Uri? Logo { get; set; }
        public string? Title { get; set; }
        public string? Message { get; set; }
    }

    public interface ILocalNotificationService
    {
        public void Send(Notification notification);
    }
}
