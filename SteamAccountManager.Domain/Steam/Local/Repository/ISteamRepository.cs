using SteamAccountManager.Domain.Steam.Model;

namespace SteamAccountManager.Domain.Steam.Local.Repository
{
    public interface ISteamRepository
    {
        public Task<List<LoginUser>> GetSteamLoginHistoryUsers();
        public void UpdateAutoLoginUser(string accountName);
        public Task<LoginUser?> GetCurrentAutoLoginUser();
    }
}