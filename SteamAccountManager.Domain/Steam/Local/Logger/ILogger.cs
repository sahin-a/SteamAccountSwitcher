using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamAccountManager.Domain.Steam.Local.Logger
{
    public interface ILogger
    {
        public void LogInformation(string tag, string message);
        public void LogDebug(string tag, string message);
        public void LogWarning(string tag, string message, System.Exception? exception = null);
        public void LogException(string tag, string message, System.Exception? exception = null);

    }
}
