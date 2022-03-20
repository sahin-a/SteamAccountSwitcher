using SteamAccountManager.Domain.Steam.Model;

namespace SteamAccountManager.Application.Steam.Service
{
    public interface ISteamService
    {
        public Task<List<Account>> GetAccounts();
        public bool SwitchAccount(string accountName);
    }
}