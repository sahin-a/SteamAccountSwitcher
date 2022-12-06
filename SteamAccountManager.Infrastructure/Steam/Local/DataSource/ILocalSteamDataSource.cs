using SteamAccountManager.Infrastructure.Steam.Local.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SteamAccountManager.Infrastructure.Steam.Local.DataSource
{
    public interface ILocalSteamDataSource
    {
        public Task<List<LoginUserDto>> GetUsersFromLoginHistory();
        public string GetSteamDir();
        public string GetSteamExecutablePath();
        public void UpdateAutoLoginUser(string accountName);
        public string GetCurrentAutoLoginUser();
    }
}