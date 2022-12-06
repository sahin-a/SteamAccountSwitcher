using System;
using System.IO;
using System.Runtime.CompilerServices;
using SteamAccountManager.Application.Steam.Local.Logger;

namespace SteamAccountManager.Infrastructure.Common.Logging
{
    internal class FileLogger : ILogger
    {
        private readonly object _lock = new();

        private void LogToFile(string message)
        {
            lock (_lock)
            {
                var fileName = $"{DateTime.UtcNow.ToString("d").ToSafeFileName()}.txt";
                using (var sw = new StreamWriter(fileName, append: true))
                {
                    sw.WriteLine(message);
                }
            }
        }

        public void LogDebug(string message, [CallerMemberName] string callerMemberName = "")
        {
            LogToFile($"[DEBUG] [{callerMemberName}] {message}");
        }

        public void LogException(string message, Exception exception, [CallerMemberName] string callerMemberName = "")
        {
            LogToFile($"[EXCEPTION] [{callerMemberName}] {message} {exception}");
        }

        public void LogInformation(string message, [CallerMemberName] string callerMemberName = "")
        {
            LogToFile($"[INFORMATION] [{callerMemberName}] {message}");
        }

        public void LogWarning(string message, Exception exception = null, [CallerMemberName] string callerMemberName = "")
        {
            LogToFile($"[WARNING] [{callerMemberName}] {message} {exception}");
        }
    }
}