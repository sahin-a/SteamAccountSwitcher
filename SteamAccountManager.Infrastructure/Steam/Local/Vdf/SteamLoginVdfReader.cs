using SteamAccountManager.Application.Steam.Local.Logger;
using SteamAccountManager.Domain.Steam.Exception.Vdf;
using SteamAccountManager.Infrastructure.Steam.Local.Dao;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SteamAccountManager.Infrastructure.Steam.Local.Vdf
{
    public class SteamLoginVdfReader : ISteamLoginVdfReader
    {
        private readonly string _vdfPath;
        private readonly ILogger _logger;

        public SteamLoginVdfReader(ISteamConfig steamConfig, ILogger logger)
        {
            _logger = logger;
            _vdfPath = steamConfig.GetLoginUsersVdfPath();
        }

        public async Task<string> GetLoginUsersVdfContent()
        {
            try
            {
                using (var streamReader = new StreamReader(Path.GetFullPath(_vdfPath)))
                {
                    return await streamReader.ReadToEndAsync();
                }
            }
            catch (Exception e)
            {
                _logger.LogException("Failed to read login vdf", e);
                throw new SteamLoginVdfReaderFailureException("Failed to retrieve contents of vdf file", e);
            }
        }
    }
}