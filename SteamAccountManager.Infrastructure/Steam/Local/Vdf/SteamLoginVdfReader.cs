using SteamAccountManager.Infrastructure.Steam.Local.Dao;
using System.IO;
using System.Threading.Tasks;

namespace SteamAccountManager.Infrastructure.Steam.Local.Vdf
{
    public class SteamLoginVdfReader : ISteamLoginVdfReader
    {
        private readonly string _vdfPath;
        private readonly StreamReader _streamReader;

        public SteamLoginVdfReader(ISteamConfig steamConfig)
        {
            _vdfPath = Path.Combine(
                steamConfig.GetSteamPath(), 
                "Steam", 
                "Config", 
                "loginusers.vdf"
                );

            _streamReader = new StreamReader(_vdfPath);
        }

        public async Task<string> GetLoginUsersVdfContent()
        {
            return await _streamReader.ReadToEndAsync();
        }
    }
}