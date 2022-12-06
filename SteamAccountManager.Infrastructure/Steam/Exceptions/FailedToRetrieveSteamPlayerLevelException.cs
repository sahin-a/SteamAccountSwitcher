using System;

namespace SteamAccountManager.Infrastructure.Steam.Exceptions
{
    public class FailedToRetrieveSteamPlayerLevelException : Exception
    {
        public FailedToRetrieveSteamPlayerLevelException() { }
        public FailedToRetrieveSteamPlayerLevelException(string message) : base(message) { }
        public FailedToRetrieveSteamPlayerLevelException(string message, Exception inner) : base(message, inner) { }
    }
}
