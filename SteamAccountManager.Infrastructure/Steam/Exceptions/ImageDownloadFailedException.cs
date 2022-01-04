using System;

namespace SteamAccountManager.Infrastructure.Steam.Exceptions
{
    public class ImageDownloadFailedException : Exception
    {
        public ImageDownloadFailedException() { }
        public ImageDownloadFailedException(string message) : base(message) { }
        public ImageDownloadFailedException(string message, Exception inner) : base(message, inner) { }
    }
}
