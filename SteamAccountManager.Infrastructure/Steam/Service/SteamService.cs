using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SteamAccountManager.Domain.Steam.Local.Logger;
using SteamAccountManager.Domain.Steam.Local.POCO;
using SteamAccountManager.Domain.Steam.Local.Repository;
using SteamAccountManager.Domain.Steam.Service;

namespace SteamAccountManager.Infrastructure.Steam.Service
{
    public class SteamService : ISteamService
    {
        private const string TAG = "SteamService";

        private readonly ISteamRepository _steamRepository;
        private readonly ILogger _logger;

        public SteamService(ISteamRepository steamRepository, ILogger logger)
        {
            _steamRepository = steamRepository;
            _logger = logger;
        }

        public async Task<List<SteamLoginUser>> GetAccounts()
        {
            return await _steamRepository.GetSteamLoginUsers();
        }

        public bool LogInAccount(string accountName)
        {
            try
            {
                _steamRepository.UpdateAutoLoginUser(accountName);
                return true;
            }
            catch (Exception e) // TODO: update to custom exception
            {
                _logger.LogException(GetType().Name, "Failed to update autologin account :(", e);
                return false;
            }
        }
    }
}