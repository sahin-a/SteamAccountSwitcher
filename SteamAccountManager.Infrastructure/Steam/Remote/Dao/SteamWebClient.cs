using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;

namespace SteamAccountManager.Infrastructure.Steam.Remote.Dao
{
    public class SteamWebClient : ISteamWebClient
    {
        private readonly RestClient _restClient;

        public SteamWebClient()
        {
            // TODO: get key from file
            _restClient = new RestClient(baseUrl: "https://api.steampowered.com");
            _restClient.AddDefaultParameter(name: "key", value: "GET_YOUR_OWN_LOL");
        }

        public async Task<T> ExecuteAsync<T>(RestRequest request) where T : new()
        {
            var response = await _restClient.ExecuteAsync(request: request);

            if (response.ErrorException != null)
            {
                throw response.ErrorException;
            }

            return JsonConvert.DeserializeObject<T>(response.Content ?? string.Empty);
        }
    }
}