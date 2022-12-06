using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using SteamAccountManager.Application.Steam.Local.Logger;

namespace SteamAccountManager.Infrastructure.Steam.Remote.Dao
{
    public class SteamWebClient : RestClient, ISteamWebClient
    {
        private readonly ILogger _logger;

        public SteamWebClient(ILogger logger) : base("https://api.steampowered.com")
        {
            // TODO: retrieve the key from a file (I would do it now, but I don't wanna bother with it yet)
            AddDefaultParameter(
                new Parameter(
                    name: "key",
                    value: "REPLACE_WITH_API_KEY",
                    ParameterType.QueryString
                )
            );
            _logger = logger;
        }

        public async Task<T> ExecuteAsync<T>(RestRequest request) where T : new()
        {
            var response = await ExecuteAsync(request: request);

            if (response.ErrorException != null)
            {
                throw response.ErrorException;
            }

            string responseString = response.Content ?? string.Empty;

            _logger.LogInformation(responseString);

            // TODO: extract the deserialization part out of this class
            return JsonConvert.DeserializeObject<T>(responseString);
        }
    }
}