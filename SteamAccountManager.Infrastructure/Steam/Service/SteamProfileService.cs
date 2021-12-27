using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SteamAccountManager.Application.Steam.Local.Logger;
using SteamAccountManager.Application.Steam.Model;
using SteamAccountManager.Application.Steam.Service;
using SteamAccountManager.Infrastructure.Steam.Exceptions;
using SteamAccountManager.Infrastructure.Steam.Remote.Dao;
using SteamAccountManager.Infrastructure.Steam.Remote.Dto;

namespace SteamAccountManager.Infrastructure.Steam.Service
{
    public class SteamProfileService : ISteamProfileService
    {
        private readonly ISteamUserProvider _steamUserProvider;
        private readonly ISteamPlayerServiceProvider _steamPlayerServiceProvider;
        private readonly ILogger _logger;

        public SteamProfileService(ISteamUserProvider steamUserProvider, ISteamPlayerServiceProvider steamPlayerServiceProvider, ILogger logger)
        {
            _steamUserProvider = steamUserProvider;
            _steamPlayerServiceProvider = steamPlayerServiceProvider;
            _logger = logger;
        }

        public async Task<List<SteamProfile>> GetProfileDetails(params string[] steamIds)
        {
            var steamProfiles = new List<SteamProfile>();
            
            try
            {
                var playerSummaries = await _steamUserProvider.GetSummariesAsync(steamIds);
                var playerBans = await _steamUserProvider.GetPlayerBansAsync(steamIds);

                var steamProfileTasks = playerSummaries.ConvertAll(async profile =>
                {
                    var playerBan = playerBans.FirstOrDefault(
                        playerBan => playerBan.SteamId == profile.SteamId,
                        new PlayerBans()
                    );

                    var playerLevel = await _steamPlayerServiceProvider.GetPlayerLevelAsync(profile.SteamId);

                    return new SteamProfile
                    {
                        Url = profile.ProfileUrl.AbsoluteUri,
                        Avatar = profile.Avatar.AbsoluteUri,
                        Username = profile.PersonaName,
                        Id = profile.SteamId,
                        IsVacBanned = playerBan.VacBanned || playerBan.NumberOfGameBans > 0,
                        IsCommunityBanned = playerBan.CommunityBanned,
                        Level = playerLevel.PlayerLevel
                    };
                });

                return new List<SteamProfile>(await Task.WhenAll(steamProfileTasks));
            }
            catch (FailedToRetrieveSteamProfileException e)
            {
                _logger.LogException("Couldn't retrieve profile details", e);
            }
            catch (FailedToRetrieveSteamPlayerBansException e)
            {
                _logger.LogException("Couldn't retrieve steam user bans", e);
            }
            catch (IllegalSteamIdsCountException e)
            {
                _logger.LogException("Illegal Steam Ids count", e);
            }

            return steamProfiles;
        }
    }
}