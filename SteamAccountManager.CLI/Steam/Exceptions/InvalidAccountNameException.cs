using System;

namespace SteamAccountManager.CLI.Steam.Exceptions
{
    public class InvalidAccountNameException : Exception
    {
        public InvalidAccountNameException(string? message = "Invalid Account Name entered", Exception? innerException = null) : base(message, innerException)
        {
        }
    }
}
