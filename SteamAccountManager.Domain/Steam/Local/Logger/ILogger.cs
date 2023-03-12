using System.Runtime.CompilerServices;

namespace SteamAccountManager.Domain.Steam.Local.Logger
{
    public interface ILogger
    {
        public void LogInformation(string message, [CallerMemberName] string callerMemberName = "");
        public void LogDebug(string message, [CallerMemberName] string callerMemberName = "");
        public void LogWarning(string message, System.Exception? exception = null, [CallerMemberName] string callerMemberName = "");
        public void LogException(string message, System.Exception exception, [CallerMemberName] string callerMemberName = "");

    }
}
