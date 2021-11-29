using System.Collections.Generic;
using System.Threading.Tasks;
using SteamAccountManager.Infrastructure.Steam.Local.Dto;
using SteamAccountManager.Infrastructure.Steam.Local.Vdf;

namespace SteamAccountManager.Infrastructure.Steam.Local.Dao
{
    public class LoginUsersDao : ILoginUsersDao
    {
        private readonly ISteamLoginVdfReader _steamLoginVdfReader;
        private readonly ISteamLoginVdfParser _steamLoginVdfParser;
        private readonly ISteamLoginVdfWriter _steamLoginVdfWriter;

        public LoginUsersDao(
            ISteamLoginVdfReader steamLoginVdfReader, 
            ISteamLoginVdfParser steamLoginVdfParser,
            ISteamLoginVdfWriter steamLoginVdfWriter
        )
        {
            _steamLoginVdfReader = steamLoginVdfReader;
            _steamLoginVdfParser = steamLoginVdfParser;
            _steamLoginVdfWriter = steamLoginVdfWriter;
        }

        public async Task<List<LoginUserDto>> GetLoggedUsers()
        {
            var vdfContent = await _steamLoginVdfReader.GetLoginUsersVdfContent();
            return _steamLoginVdfParser.ParseLoginUsers(vdfContent);
        }

        public bool SetAutoLoginUser(string steamId)
        {
            return false;
        }
    }
}