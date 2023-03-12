using SteamAccountManager.Infrastructure.Steam.Remote.Dao;
using SteamAccountManager.Infrastructure.Steam.Remote.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SteamAccountManager.Domain.Steam.Local.Logger;
using SteamAccountManager.Domain.Steam.Model;
using SteamAccountManager.Domain.Steam.Service;
using SteamAccountManager.Infrastructure.Steam.Exceptions;

namespace SteamAccountManager.Infrastructure.Steam.Service
{
    public class SteamProfileService : ISteamProfileService
    {
        private readonly ISteamUserProvider _steamUserProvider;
        private readonly ISteamPlayerService _steamPlayerService;
        private readonly ILogger _logger;

        public SteamProfileService(ISteamUserProvider steamUserProvider, ISteamPlayerService steamPlayerService, ILogger logger)
        {
            _steamUserProvider = steamUserProvider;
            _steamPlayerService = steamPlayerService;
            _logger = logger;
        }

        private async Task<int> GetSteamLevel(string steamId)
        {
            try
            {
                return await _steamPlayerService.GetPlayerLevelAsync(steamId);
            }
            catch (FailedToRetrieveSteamPlayerLevelException e)
            {
                _logger.LogException("Couldn't retrieve steam player level", e);
            }
            catch (IllegalSteamIdsCountException e)
            {
                _logger.LogException("Illegal Steam Ids count", e);
            }

            return -1;
        }

        private async Task<List<PlayerSummary>> GetPlayerSummaries(params string[] steamIds)
        {
            try
            {
                return await _steamUserProvider.GetSummariesAsync(steamIds);
            }
            catch (FailedToRetrieveSteamProfileException e)
            {
                _logger.LogException("Couldn't retrieve profile details", e);
            }
            catch (IllegalSteamIdsCountException e)
            {
                _logger.LogException("Illegal Steam Ids count", e);
            }

            return new();
        }

        private async Task<List<PlayerBans>> GetPlayerBans(params string[] steamIds)
        {
            try
            {
                return await _steamUserProvider.GetPlayerBansAsync(steamIds);
            }
            catch (FailedToRetrieveSteamPlayerBansException e)
            {
                _logger.LogException("Couldn't retrieve steam user bans", e);
            }
            catch (IllegalSteamIdsCountException e)
            {
                _logger.LogException("Illegal Steam Ids count", e);
            }

            return new();
        }

        public async Task<List<Profile>> GetProfileDetails(params string[] steamIds)
        {
            var steamProfiles = new List<Profile>();
            var playerSummaries = await GetPlayerSummaries(steamIds);
            var playerBans = await GetPlayerBans(steamIds);
            var steamProfileTasks = playerSummaries.ConvertAll(async profile =>
            {
                var playerLevel = await GetSteamLevel(profile.SteamId);
                var playerBan = playerBans.FirstOrDefault(
                    playerBan => playerBan.SteamId == profile.SteamId,
                    new PlayerBans()
                );

                return new Profile
                {
                    Url = profile.ProfileUrl.AbsoluteUri,
                    AvatarUrl = profile.Avatar.AbsoluteUri,
                    Username = profile.PersonaName,
                    Id = profile.SteamId,
                    IsVacBanned = playerBan.VacBanned || playerBan.NumberOfGameBans > 0,
                    IsCommunityBanned = playerBan.CommunityBanned,
                    Level = playerLevel
                };
            });

            return new List<Profile>(await Task.WhenAll(steamProfileTasks));
        }
    }
}