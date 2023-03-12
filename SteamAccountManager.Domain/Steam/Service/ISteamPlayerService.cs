namespace SteamAccountManager.Domain.Steam.Service
{
    public interface ISteamPlayerService
    {
        public Task<int> GetPlayerLevelAsync(string steamId);
    }
}