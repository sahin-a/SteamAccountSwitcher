using SteamAccountManager.Application.Steam.Model;

namespace SteamAccountManager.Application.Steam.Service
{
    public interface ISteamPlayerService
    {
        public Task<int> GetPlayerLevelAsync(string steamId);
    }
}