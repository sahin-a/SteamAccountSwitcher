using SteamAccountManager.Application.Steam.Model;

namespace SteamAccountManager.Application.Steam.Local.Repository
{
    public interface ISteamRepository
    {
        public Task<List<LoginUser>> GetSteamLoginHistoryUsers();
        public void UpdateAutoLoginUser(string accountName);
        public Task<LoginUser> GetCurrentAutoLoginUser();
    }
}