using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamAccountManager.Domain.Steam.Service
{
    public interface ISteamProcessService
    {
        public bool KillSteam();
        public void StartSteam();
    }
}
