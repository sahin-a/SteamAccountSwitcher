using RestSharp;
using SteamAccountManager.Application.Steam.Local.Logger;
using SteamAccountManager.Infrastructure.Steam.Remote.Dto;
using System;
using System.Threading.Tasks;
using SteamAccountManager.Infrastructure.Steam.Exceptions;

namespace SteamAccountManager.Infrastructure.Steam.Remote.Dao
{
    public class SteamPlayerServiceProvider : ISteamPlayerServiceProvider
    {
        private readonly ISteamWebClient _steamWebClient;
        private readonly ILogger _logger;

        public SteamPlayerServiceProvider(ISteamWebClient steamWebClient, ILogger logger)
        {
            _steamWebClient = steamWebClient;
            _logger = logger;
        }

        private RestRequest CreateRequest(string action, Method method) =>
            new RestRequest(resource: $"IPlayerService/{action}", method);

        public async Task<SteamPlayerLevel> GetPlayerLevelAsync(string steamId)
        {
            if (steamId.Length <= 0)
                throw new Exception("Illegal Steam Id");

            // we can only send a maximum of 100 steam ids per request, break it into smaller parts if that's the case
            var request = CreateRequest(action: "GetSteamLevel/v1", Method.Get);
            request.AddParameter(name: "steamid", value: steamId, ParameterType.QueryString);

            try
            {
                var response = await _steamWebClient.ExecuteAsync<SteamPlayerLevelDto>(request);

                return response.Response;
            }
            catch (Exception e)
            {
                var exception = new RequestNotSuccessfulException("Failed to receive steam level", e);
                _logger.LogException("Failed to receive steam profile data", exception);

                throw exception;
            }
        }
    }
}
