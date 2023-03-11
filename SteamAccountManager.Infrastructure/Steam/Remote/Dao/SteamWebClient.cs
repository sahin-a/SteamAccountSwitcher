using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using SteamAccountManager.Application.Steam.Local.Logger;
using SteamAccountManager.Infrastructure.Steam.Local.Storage;

namespace SteamAccountManager.Infrastructure.Steam.Remote.Dao
{
    public class SteamWebClient : RestClient, ISteamWebClient
    {
        private readonly ILogger _logger;
        private readonly SteamApiKeyStorage _steamApiKeyStorage;

        public SteamWebClient(ILogger logger, SteamApiKeyStorage apiKeyStorage) : base("https://api.steampowered.com")
        {
            _steamApiKeyStorage = apiKeyStorage;
            _logger = logger;
        }

        public async Task<T> ExecuteAsync<T>(RestRequest request) where T : new()
        {
            request.AddParameter(name: "key", value: _steamApiKeyStorage.Get());

            var response = await ExecuteAsync(request: request);

            if (response.ErrorException != null)
            {
                throw response.ErrorException;
            }

            string responseString = response.Content ?? string.Empty;

            _logger.LogInformation(responseString);

            return JsonConvert.DeserializeObject<T>(responseString);
        }
    }
}