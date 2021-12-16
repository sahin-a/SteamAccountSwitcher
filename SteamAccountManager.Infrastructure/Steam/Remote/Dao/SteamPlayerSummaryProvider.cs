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
    public class SteamPlayerSummaryProvider : ISteamPlayerSummaryProvider
    {
        private const byte MAX_STEAM_IDS_PER_REQUEST = 100;
        private readonly ISteamWebClient _steamWebClient;
        private readonly ILogger _logger;

        public SteamPlayerSummaryProvider(ISteamWebClient steamWebClient, ILogger logger)
        {
            _steamWebClient = steamWebClient;
            _logger = logger;
        }

        public async Task<List<PlayerSummary>> GetSummaryAsync(params string[] steamIds)
        {
            if (steamIds.Length <= 0)
                throw new InvalidSteamPlayerSummaryRequestException("You have to supply at least 1 steam id!");

            // we can only send a maximum of 100 steam ids per request, break it into smaller parts if that's the case
            string[][] chunks = steamIds
                .Select((s, i) => new {Value = s, Index = i})
                .GroupBy(x => x.Index / MAX_STEAM_IDS_PER_REQUEST)
                .Select(grp => grp.Select(x => x.Value).ToArray())
                .ToArray();

            var playerSummaries = new List<PlayerSummary>();

            foreach (var steamIdsPerRequest in chunks)
            {
                var commaSeperatedSteamIds = string.Join(",", steamIdsPerRequest);

                var request = new RestRequest(resource: "ISteamUser/GetPlayerSummaries/v0002", Method.Get);
                request.AddParameter(name: "steamids", value: commaSeperatedSteamIds);

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
    }
}