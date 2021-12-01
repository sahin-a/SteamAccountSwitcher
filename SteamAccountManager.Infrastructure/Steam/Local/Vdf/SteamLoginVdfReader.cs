using System;
using SteamAccountManager.Infrastructure.Steam.Local.Dao;
using System.IO;
using System.Threading.Tasks;
using SteamAccountManager.Domain.Steam.Exception.Vdf;
using SteamAccountManager.Domain.Steam.Local.Logger;

namespace SteamAccountManager.Infrastructure.Steam.Local.Vdf
{
    public class SteamLoginVdfReader : ISteamLoginVdfReader
    {
        private readonly string _vdfPath;
        private readonly StreamReader _streamReader;
        private readonly ILogger _logger;

        public SteamLoginVdfReader(ISteamConfig steamConfig, ILogger logger)
        {
            _logger = logger;
            _vdfPath = Path.Combine(
                steamConfig.GetSteamPath(),
                "Config", 
                "loginusers.vdf"
                );

            _streamReader = new StreamReader(Path.GetFullPath(_vdfPath));
        }

        public async Task<string> GetLoginUsersVdfContent()
        {
            try
            {
                return await _streamReader.ReadToEndAsync();
            }
            catch (Exception e)
            {
                _logger.LogException(GetType().Name, "Failed to read login vdf", e);
                throw new SteamLoginVdfReaderFailureException();
            }
        }
    }
}