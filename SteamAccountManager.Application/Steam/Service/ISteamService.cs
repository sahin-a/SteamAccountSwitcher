using System.Collections.Generic;
using System.Threading.Tasks;
using SteamAccountManager.Application.Steam.Model;

namespace SteamAccountManager.Application.Steam.Service
{
    public interface ISteamService
    {
        public Task<List<SteamLoginUser>> GetAccounts();
        public bool SwitchAccount(SteamLoginUser steamLoginUser);
    }
}