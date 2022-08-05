namespace SteamAccountManager.Infrastructure.Steam.Exceptions
{
    public class ImageDownloadFailedException : System.Exception
    {
        public ImageDownloadFailedException() { }
        public ImageDownloadFailedException(string message) : base(message) { }
        public ImageDownloadFailedException(string message, System.Exception inner) : base(message, inner) { }
    }
}
