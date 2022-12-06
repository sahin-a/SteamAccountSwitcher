namespace SteamAccountManager.Infrastructure.Steam.Exceptions
{
    public class FailedToRetrieveSteamProfileException : System.Exception
    {
        public FailedToRetrieveSteamProfileException()
        {
        }

        public FailedToRetrieveSteamProfileException(string message) : base(message)
        {
        }

        public FailedToRetrieveSteamProfileException(string message, System.Exception inner) : base(message, inner)
        {
        }
    }
}