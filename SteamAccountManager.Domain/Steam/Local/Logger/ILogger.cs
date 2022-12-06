using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

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
