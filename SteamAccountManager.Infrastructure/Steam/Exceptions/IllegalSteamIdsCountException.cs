namespace SteamAccountManager.Infrastructure.Steam.Exceptions
{
    public class IllegalSteamIdsCountException : System.Exception
    {
        public IllegalSteamIdsCountException()
        {
        }

        public IllegalSteamIdsCountException(string message) : base(message)
        {
        }

        public IllegalSteamIdsCountException(string message, System.Exception inner) : base(message, inner)
        {
        }
    }
}