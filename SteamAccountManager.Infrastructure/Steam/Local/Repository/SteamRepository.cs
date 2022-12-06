using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SteamAccountManager.Domain.Steam.Exception;
using SteamAccountManager.Domain.Steam.Local.Logger;
using SteamAccountManager.Domain.Steam.Local.POCO;
using SteamAccountManager.Domain.Steam.Local.Repository;
using SteamAccountManager.Infrastructure.Steam.Local.DataSource;
using SteamAccountManager.Infrastructure.Steam.Local.Mapping;

namespace SteamAccountManager.Infrastructure.Steam.Local.Repository
{
    public class SteamRepository : ISteamRepository
    {
        private readonly ILocalSteamDataSource _steamDataSource;
        private readonly ILogger _logger;

        public SteamRepository(ILocalSteamDataSource steamDataSource, ILogger logger)
        {
            _steamDataSource = steamDataSource;
            _logger = logger;
        }

        public async Task<List<SteamLoginUser>> GetSteamLoginUsers()
        {
            return (await _steamDataSource.GetLoggedInUsers())
                .ToSteamLoginUsers();
        }

        public void UpdateAutoLoginUser(SteamLoginUser steamLoginUser)
        {
            _steamDataSource.UpdateAutoLoginUser(steamLoginUser.AccountName);
        }

        public async Task<SteamLoginUser> GetCurrentAutoLoginUser()
        {
            var currentAutoLoginAccountName = _steamDataSource.GetCurrentAutoLoginUser();
            try
            {
                var currentSteamLoginUser = (await _steamDataSource.GetLoggedInUsers())
                    .First(dto => dto.AccountName == currentAutoLoginAccountName)
                    .ToSteamLoginUser();

                return currentSteamLoginUser;
            }
            catch (Exception e)
            {
                var exception = new SteamAutoLoginUserNotFoundException("Steam Auto Login User not found", e);
                _logger.LogException("User not found", exception);
                
                throw exception;
            }
        }
    }
}