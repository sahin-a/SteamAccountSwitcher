using System.Collections.Generic;
using System.Threading.Tasks;
using SteamAccountManager.Infrastructure.Steam.Local.Dto;

namespace SteamAccountManager.Infrastructure.Steam.Local.DataSource
{
    public interface ILocalSteamDataSource
    {
        public Task<List<LoginUserDto>> GetUsersFromLoginHistory();
        public string GetSteamDir();
        public string GetSteamExecutablePath();
        public void UpdateAutoLoginUser(string accountName);
        public string? GetCurrentAutoLoginUser();
    }
}