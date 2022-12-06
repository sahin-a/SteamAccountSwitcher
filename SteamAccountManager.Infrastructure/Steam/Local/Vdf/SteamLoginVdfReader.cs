using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using SteamAccountManager.Infrastructure.Steam.Local.Dao;
using SteamAccountManager.Infrastructure.Steam.Local.Dto;

namespace SteamAccountManager.Infrastructure.Steam.Local.Vdf
{
    public class SteamLoginVdfReader : ISteamLoginVdfReader
    {
        private readonly string _vdfPath;
        private readonly ISteamLoginVdfParser _steamLoginVdfParser;

        public SteamLoginVdfReader(ISteamConfig steamConfig, ISteamLoginVdfParser steamLoginVdfParser)
        {
            _steamLoginVdfParser = steamLoginVdfParser;
            _vdfPath = Path.Combine(
                steamConfig.GetSteamPath(), 
                "Steam", 
                "Config", 
                "loginusers.vdf"
                );
        }

        private async Task<string> GetLoginUsersVdfContent()
        {
            using var sr = new StreamReader(_vdfPath);
            return await sr.ReadToEndAsync();
        }

        public async Task<List<LoginUserDto>> GetParsedLoginUsersVdf()
        {
            return _steamLoginVdfParser.ParseLoginUsers(await GetLoginUsersVdfContent());
        }
    }
}