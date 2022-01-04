using System;

namespace SteamAccountManager.Infrastructure.Steam.Exceptions
{
    public class RequestNotSuccessfulException : Exception
    {
        public RequestNotSuccessfulException() { }
        public RequestNotSuccessfulException(string message) : base(message) { }
        public RequestNotSuccessfulException(string message, Exception inner) : base(message, inner) { }
    }
}
