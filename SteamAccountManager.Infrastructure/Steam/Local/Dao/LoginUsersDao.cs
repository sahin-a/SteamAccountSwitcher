using System.Collections.Generic;
using System.Threading.Tasks;
using SteamAccountManager.Infrastructure.Steam.Local.Dto;
using SteamAccountManager.Infrastructure.Steam.Local.Vdf;

namespace SteamAccountManager.Infrastructure.Steam.Local.Dao
{
    public class LoginUsersDao : ILoginUsersDao
    {
        private ISteamLoginVdfReader _steamLoginVdfReader;

        public LoginUsersDao(ISteamLoginVdfReader steamLoginVdfReader)
        {
            _steamLoginVdfReader = steamLoginVdfReader;
        }

        public async Task<List<LoginUserDto>> GetLoggedUsers()
        {
            return await _steamLoginVdfReader.GetParsedLoginUsersVdf();
        }

        public bool SetAutoLoginUser(string steamId)
        {
            return false;
        }
    }
}