namespace SteamAccountManager.Infrastructure.Steam.Exceptions
{
    public class RequestNotSuccessfulException : System.Exception
    {
        public RequestNotSuccessfulException() { }
        public RequestNotSuccessfulException(string message) : base(message) { }
        public RequestNotSuccessfulException(string message, System.Exception inner) : base(message, inner) { }
    }
}
