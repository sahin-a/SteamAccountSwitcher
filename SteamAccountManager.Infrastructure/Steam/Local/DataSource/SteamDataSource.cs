using System.Collections.Generic;
using System.Threading.Tasks;
using SteamAccountManager.Infrastructure.Steam.Local.Dao;
using SteamAccountManager.Infrastructure.Steam.Local.Dto;

namespace SteamAccountManager.Infrastructure.Steam.Local.DataSource
{
    public class SteamDataSource : ISteamDataSource
    {
        private readonly ISteamConfig _steamConfig;
        private readonly ILoginUsersDao _loginUsersDao;

        public SteamDataSource(ISteamConfig steamConfig, ILoginUsersDao loginUsersDao)
        {
            _steamConfig = steamConfig;
            _loginUsersDao = loginUsersDao;
        }

        public async Task<List<LoginUserDto>> GetLoggedInUsers()
        {
            return await _loginUsersDao.GetLoggedUsers();
        }

        public string GetSteamDir()
        {
            return _steamConfig.GetSteamPath();
        }

        public string GetSteamExecutablePath()
        {
            return _steamConfig.GetSteamExecutablePath();
        }

        public bool UpdateAutoLoginUser(string steamId)
        {
            return _loginUsersDao.SetAutoLoginUser(steamId);
        }
    }
}