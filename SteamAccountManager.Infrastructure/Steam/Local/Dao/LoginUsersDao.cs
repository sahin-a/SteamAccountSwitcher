using SteamAccountManager.Infrastructure.Steam.Local.Dto;
using SteamAccountManager.Infrastructure.Steam.Local.Vdf;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SteamAccountManager.Infrastructure.Steam.Local.Dao
{
    public class LoginUsersDao : ILoginUsersDao
    {
        private readonly ISteamLoginVdfReader _steamLoginVdfReader;
        private readonly ISteamLoginVdfParser _steamLoginVdfParser;

        public LoginUsersDao(
            ISteamLoginVdfReader steamLoginVdfReader,
            ISteamLoginVdfParser steamLoginVdfParser
        )
        {
            _steamLoginVdfReader = steamLoginVdfReader;
            _steamLoginVdfParser = steamLoginVdfParser;
        }

        public async Task<List<LoginUserDto>> GetLoggedUsers()
        {
            var vdfContent = await _steamLoginVdfReader.GetLoginUsersVdfContent();
            return _steamLoginVdfParser.ParseLoginUsers(vdfContent);
        }
    }
}