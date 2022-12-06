using SteamAccountManager.Application.Steam.Model;

namespace SteamAccountManager.Application.Steam.Service
{
    public interface ISteamService
    {
        public Task<List<SteamAccount>> GetAccounts();
        public bool SwitchAccount(string accountName);
    }
}