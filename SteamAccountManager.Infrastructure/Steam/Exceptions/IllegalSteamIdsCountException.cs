using System;

namespace SteamAccountManager.Infrastructure.Steam.Exceptions
{
    public class IllegalSteamIdsCountException : Exception
    {
        public IllegalSteamIdsCountException()
        {
        }

        public IllegalSteamIdsCountException(string message) : base(message)
        {
        }

        public IllegalSteamIdsCountException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}