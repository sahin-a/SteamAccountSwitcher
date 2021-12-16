using System;
using System.Runtime.Serialization;

namespace SteamAccountManager.Infrastructure.Steam.Exceptions
{
    public class FailedToRetrieveSteamProfileException : Exception
    {
        public FailedToRetrieveSteamProfileException()
        {
        }

        public FailedToRetrieveSteamProfileException(string message) : base(message)
        {
        }

        public FailedToRetrieveSteamProfileException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}