using RestSharp;
using System.Threading.Tasks;

namespace SteamAccountManager.Infrastructure.Steam.Remote.Dao
{
    public interface ISteamWebClient
    {
        public Task<T> ExecuteAsync<T>(RestRequest request) where T : new();
    }
}