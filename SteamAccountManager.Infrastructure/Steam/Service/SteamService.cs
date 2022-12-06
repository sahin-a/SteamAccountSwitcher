using SteamAccountManager.Application.Steam.Local.Logger;
using SteamAccountManager.Application.Steam.Local.Repository;
using SteamAccountManager.Application.Steam.Model;
using SteamAccountManager.Application.Steam.Service;
using SteamAccountManager.Domain.Steam.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SteamAccountManager.Infrastructure.Steam.Service
{
    public class SteamService : ISteamService
    {
        private readonly ISteamRepository _steamRepository;
        private readonly ISteamProcessService _steamProcessService;
        private readonly ILogger _logger;
        private readonly ISteamProfileService _steamProfileService;

        public SteamService(ISteamRepository steamRepository, ISteamProcessService steamProcessService,
            ISteamProfileService steamProfileService, ILogger logger)
        {
            _steamRepository = steamRepository;
            _steamProcessService = steamProcessService;
            _steamProfileService = steamProfileService;
            _logger = logger;
        }

        public async Task<List<SteamAccount>> GetAccounts()
        {
            try
            {
                var steamLoginUsers = await _steamRepository.GetSteamLoginHistoryUsers();
                var steamIds = steamLoginUsers.Select(user => user.SteamId);
                var steamProfiles = await _steamProfileService.GetProfileDetails(steamIds.ToArray());

                var steamAccounts = steamLoginUsers.ConvertAll(steamLoginUser =>
                {
                    var steamProfile = steamProfiles.FirstOrDefault(
                        profile => profile.Id == steamLoginUser.SteamId,
                        new SteamProfile()
                    );

                    return new SteamAccount.Builder()
                        .SetData(steamLoginUser)
                        .SetData(steamProfile)
                        .Build();
                });

                return steamAccounts;
            }
            catch (Exception e)
            {
                _logger.LogException("Failed to retrieve steam profile data", e);
            }

            return new List<SteamAccount>();
        }

        public bool SwitchAccount(string accountName)
        {
            try
            {
                _steamRepository.UpdateAutoLoginUser(accountName);
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