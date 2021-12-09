using System.Collections.Generic;
using System.Threading.Tasks;
using SteamAccountManager.Application.Steam.Model;

namespace SteamAccountManager.Application.Steam.Local.Repository
{
    public interface ISteamRepository
    {
        public Task<List<SteamLoginUser>> GetSteamLoginHistoryUsers();
        public void UpdateAutoLoginUser(SteamLoginUser steamLoginUser);
        public Task<SteamLoginUser> GetCurrentAutoLoginUser();
    }
}