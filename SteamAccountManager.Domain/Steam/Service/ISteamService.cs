using System.Collections.Generic;
using System.Threading.Tasks;
using SteamAccountManager.Domain.Steam.Local.POCO;

namespace SteamAccountManager.Domain.Steam.Service
{
    public interface ISteamService
    {
        public Task<List<SteamLoginUser>> GetAccounts();
        public bool SwitchAccount(SteamLoginUser steamLoginUser);
    }
}