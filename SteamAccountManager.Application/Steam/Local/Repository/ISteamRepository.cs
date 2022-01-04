using SteamAccountManager.Application.Steam.Model;

namespace SteamAccountManager.Application.Steam.Local.Repository
{
    public interface ISteamRepository
    {
        public Task<List<SteamLoginUser>> GetSteamLoginHistoryUsers();
        public void UpdateAutoLoginUser(string accountName);
        public Task<SteamLoginUser> GetCurrentAutoLoginUser();
    }
}