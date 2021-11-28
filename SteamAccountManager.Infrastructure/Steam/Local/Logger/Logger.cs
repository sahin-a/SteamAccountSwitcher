using SteamAccountManager.Domain.Steam.Local.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamAccountManager.Infrastructure.Steam.Local.Logger
{
    public class Logger : ILogger
    {
        public void LogDebug(string tag, string message)
        {
            throw new NotImplementedException();
        }

        public void LogException(string tag, string message, Exception exception = null)
        {
            throw new NotImplementedException();
        }

        public void LogInformation(string tag, string message)
        {
            throw new NotImplementedException();
        }

        public void LogWarning(string tag, string message, Exception exception = null)
        {
            throw new NotImplementedException();
        }
    }
}
