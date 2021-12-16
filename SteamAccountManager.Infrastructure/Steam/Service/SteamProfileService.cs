using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SteamAccountManager.Application.Steam.Model;
using SteamAccountManager.Application.Steam.Service;
using SteamAccountManager.Infrastructure.Steam.Exceptions;
using SteamAccountManager.Infrastructure.Steam.Remote.Dao;

namespace SteamAccountManager.Infrastructure.Steam.Service
{
    public class SteamProfileService : ISteamProfileService
    {
        private readonly ISteamPlayerSummaryProvider _steamPlayerSummaryProvider;

        public SteamProfileService(ISteamPlayerSummaryProvider steamPlayerSummaryProvider)
        {
            _steamPlayerSummaryProvider = steamPlayerSummaryProvider;
        }
        
        public async Task<List<SteamProfile>> GetProfileDetails(params string[] steamIds)
        {
            try
            {
                var allProfiles = new List<SteamProfile>();
                var profiles = await _steamPlayerSummaryProvider.GetSummaryAsync(steamIds);

                return profiles.ConvertAll(profile =>
                    new SteamProfile
                    {
                        Url = profile.ProfileUrl.AbsoluteUri,
                        Avatar = profile.Avatar.AbsoluteUri,
                        Username = profile.PersonaName,
                        Id = profile.SteamId
                    }
                );
            }
            catch (FailedToRetrieveSteamProfileException e)
            {
            }
            catch (InvalidSteamPlayerSummaryRequestException e)
            {
            }

            return new List<SteamProfile>();
        }
    }
}