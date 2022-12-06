using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using SteamAccountManager.Domain.Steam.Exception;
using SteamAccountManager.Domain.Steam.Local.Logger;
using SteamAccountManager.Domain.Steam.Local.POCO;
using SteamAccountManager.Domain.Steam.Local.Repository;
using SteamAccountManager.Domain.Steam.Service;

namespace SteamAccountManager.Infrastructure.Steam.Service
{
    public class SteamService : ISteamService
    {
        private readonly ISteamRepository _steamRepository;
        private readonly ISteamProcessService _steamProcessService;
        private readonly ILogger _logger;

        public SteamService(ISteamRepository steamRepository, ISteamProcessService steamProcessService, ILogger logger)
        {
            _steamRepository = steamRepository;
            _steamProcessService = steamProcessService;
            _logger = logger;
        }

        public async Task<List<SteamLoginUser>> GetAccounts()
        {
            return await _steamRepository.GetSteamLoginUsers();
        }

        public bool SwitchAccount(SteamLoginUser steamLoginUser)
        {
            try
            {
                _steamRepository.UpdateAutoLoginUser(steamLoginUser);
            }
            catch (UpdateAutoLoginUserFailedException e)
            {
                _logger.LogException("Failed to update autologin account :(", e);
                return false;
            }

            if (_steamProcessService.KillSteam())
                _steamProcessService.StartSteam();

            return true;
        }
    }
}