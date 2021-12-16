using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestSharp;
using SteamAccountManager.Application.Steam.Local.Logger;
using SteamAccountManager.Infrastructure.Steam.Exceptions;
using SteamAccountManager.Infrastructure.Steam.Remote.Dto;

namespace SteamAccountManager.Infrastructure.Steam.Remote.Dao
{
    public class SteamUserProvider : ISteamUserProvider
    {
        private const byte MAX_STEAM_IDS_PER_REQUEST = 100;
        private readonly ISteamWebClient _steamWebClient;
        private readonly ILogger _logger;
        
        // TODO: create/rename class SteamUserClient

        public SteamUserProvider(ISteamWebClient steamWebClient, ILogger logger)
        {
            _steamWebClient = steamWebClient;
            _logger = logger;
        }
        
        private RestRequest CreateRequest(string action, Method method) =>
            new RestRequest(resource: $"ISteamUser/{action}", method);

        public async Task<List<PlayerSummary>> GetSummariesAsync(params string[] steamIds)
        {
            if (steamIds.Length <= 0)
                throw new IllegalSteamIdsCountException("You have to supply at least 1 steam id!");
            
            // we can only send a maximum of 100 steam ids per request, break it into smaller parts if that's the case
            var chunks = steamIds.Chunk(100);
            var playerSummaries = new List<PlayerSummary>();

            foreach (var steamIdsChunk in chunks)
            {
                var commaSeperatedSteamIds = string.Join(",", steamIdsChunk);

                var request = CreateRequest(action: "GetPlayerSummaries/v0002", Method.Get);
                request.AddParameter(name: "steamids", value: commaSeperatedSteamIds, ParameterType.QueryString);

                try
                {
                    var response = await _steamWebClient.ExecuteAsync<SteamPlayerSummariesDto>(request);
                    playerSummaries.AddRange(response.Response.PlayerSummaries);
                }
                catch (Exception e)
                {
                    var exception = new FailedToRetrieveSteamProfileException("Failed to retrieve steam profile/s", e);
                    _logger.LogException("Failed to receive steam profile data", exception);

                    throw exception;
                }
            }

            return playerSummaries;
        }

        public async Task<List<PlayerBans>> GetPlayerBansAsync(params string[] steamIds)
        {
            if (steamIds.Length <= 0)
                throw new IllegalSteamIdsCountException("You have to supply at least 1 steam id!");
            
            var commaSeperatedSteamIds = string.Join(",", steamIds);
            
            var request = CreateRequest(action: "GetPlayerBans/v1/", Method.Get);
            request.AddParameter(name: "steamids", value: commaSeperatedSteamIds, ParameterType.QueryString);

            try
            {
                var response = await _steamWebClient.ExecuteAsync<SteamPlayerBansDto>(request);
                return response.Players;
            }
            catch (Exception e)
            {
                var exception = new FailedToRetrieveSteamPlayerBansException("Failed to retrieve player/s bans", e);
                _logger.LogException("Failed to receive steam player bans", exception);

                throw exception;
            }
        }
    }
}