namespace SteamAccountManager.Infrastructure.Steam.Exceptions.Vdf
{
    public class SteamLoginVdfReaderFailureException : System.Exception
    {
        public SteamLoginVdfReaderFailureException()
        {
        }

        public SteamLoginVdfReaderFailureException(string message) : base(message)
        {
        }

        public SteamLoginVdfReaderFailureException(string message, System.Exception inner) : base(message, inner)
        {
        }
    }
}