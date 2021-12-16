using System;

namespace SteamAccountManager.Infrastructure.Steam.Exceptions
{
    public class InvalidSteamPlayerSummaryRequestException : Exception
    {
        public InvalidSteamPlayerSummaryRequestException()
        {
        }

        public InvalidSteamPlayerSummaryRequestException(string message) : base(message)
        {
        }

        public InvalidSteamPlayerSummaryRequestException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}