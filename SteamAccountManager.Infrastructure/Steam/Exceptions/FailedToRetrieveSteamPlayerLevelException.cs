namespace SteamAccountManager.Infrastructure.Steam.Exceptions
{
    public class FailedToRetrieveSteamPlayerLevelException : System.Exception
    {
        public FailedToRetrieveSteamPlayerLevelException() { }
        public FailedToRetrieveSteamPlayerLevelException(string message) : base(message) { }
        public FailedToRetrieveSteamPlayerLevelException(string message, System.Exception inner) : base(message, inner) { }
    }
}
