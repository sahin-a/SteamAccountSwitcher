using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamAccountManager.Infrastructure.Steam.Exceptions
{
    public class FailedToRetrieveSteamPlayerLevelException : Exception
    {
        public FailedToRetrieveSteamPlayerLevelException() { }
        public FailedToRetrieveSteamPlayerLevelException(string message) : base(message) { }
        public FailedToRetrieveSteamPlayerLevelException(string message, Exception inner) : base(message, inner) { }
    }
}
