using System.Collections.Generic;
using System.Threading.Tasks;
using SteamAccountManager.Infrastructure.Steam.Local.Dto;

namespace SteamAccountManager.Infrastructure.Steam.Local.DataSource
{
    public interface ISteamDataSource
    {
        public Task<List<LoginUserDto>> GetLoggedInUsers();
        public string GetSteamDir();
        public string GetSteamExecutablePath();

        public bool UpdateAutoLoginUser(string steamId);
    }
}