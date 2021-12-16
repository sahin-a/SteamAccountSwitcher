using System;

namespace SteamAccountManager.Infrastructure.Steam.Exceptions
{
    public class FailedToRetrieveSteamPlayerBansException : Exception
    {
        public FailedToRetrieveSteamPlayerBansException()
        {
        }

        public FailedToRetrieveSteamPlayerBansException(string message) : base(message)
        {
        }

        public FailedToRetrieveSteamPlayerBansException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}