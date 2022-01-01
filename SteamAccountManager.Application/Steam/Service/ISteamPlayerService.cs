using SteamAccountManager.Application.Steam.Model;

namespace SteamAccountManager.Application.Steam.Service
{
    public interface ISteamPlayerService
    {
        public Task<SteamLevel> GetPlayerLevelAsync(string steamId);
    }
}