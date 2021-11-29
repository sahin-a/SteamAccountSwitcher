using SteamAccountManager.Domain.Steam.Local.Logger;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamAccountManager.Infrastructure.Steam.Local.Logger
{
    public class Logger : ILogger
    {
        public void LogDebug(string tag, string message)
        {
#if DEBUG
            Debug.WriteLine($"[DEBUG] {tag} {message}");
#endif
        }

        public void LogException(string tag, string message, Exception exception = null)
        {
            Debug.WriteLine($"[EXCEPTION] {tag} {message} {exception}");
        }

        public void LogInformation(string tag, string message)
        {
            Debug.WriteLine($"[INFORMATION] {tag} {message}");
        }

        public void LogWarning(string tag, string message, Exception exception = null)
        {
            Debug.WriteLine($"[WARNING] {tag} {message} {exception}");
        }
    }
}
