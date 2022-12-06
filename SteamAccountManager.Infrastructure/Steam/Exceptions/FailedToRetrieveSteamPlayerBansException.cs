namespace SteamAccountManager.Infrastructure.Steam.Exceptions
{
    public class FailedToRetrieveSteamPlayerBansException : System.Exception
    {
        public FailedToRetrieveSteamPlayerBansException()
        {
        }

        public FailedToRetrieveSteamPlayerBansException(string message) : base(message)
        {
        }

        public FailedToRetrieveSteamPlayerBansException(string message, System.Exception inner) : base(message, inner)
        {
        }
    }
}