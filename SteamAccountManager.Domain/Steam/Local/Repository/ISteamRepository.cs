using System.Collections.Generic;
using System.Threading.Tasks;
using SteamAccountManager.Domain.Steam.Local.POCO;

namespace SteamAccountManager.Domain.Steam.Local.Repository
{
    public interface ISteamRepository
    {
        public Task<List<SteamLoginUser>> GetSteamLoginUsers();
        public void UpdateAutoLoginUser(string steamId);
    }
}