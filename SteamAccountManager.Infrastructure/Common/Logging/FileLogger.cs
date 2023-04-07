using System;
using System.IO;
using System.Runtime.CompilerServices;
using SteamAccountManager.Domain.Steam.Local.Logger;
using SteamAccountManager.Infrastructure.Steam.Local.Dao;
using SteamAccountManager.Infrastructure.Steam.Local.Storage;

namespace SteamAccountManager.Infrastructure.Common.Logging
{
    internal class FileLogger : ILogger
    {
        private readonly object _lock = new();
        private readonly IFileProvider _fileProvider;
        private readonly string _dir = AppDataDirectory.Logs;

        public FileLogger(IFileProvider fileProvider)
        {
            _fileProvider = fileProvider;
        }

        private void LogToFile(string message)
        {
            lock (_lock)
            {
                var fileName = $"{DateTime.UtcNow.ToString("d").ToSafeFileName()}.txt";
                var filePath = Path.Combine(_dir, fileName);

                Directory.CreateDirectory(_dir);
                _fileProvider.WriteAllText(path: filePath, content: message, append: true);
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

        public void LogWarning(string message, Exception? exception = null,
            [CallerMemberName] string callerMemberName = "")
        {
            LogToFile($"[WARNING] [{callerMemberName}] {message} {exception}");
        }
    }
}