using SteamAccountManager.Infrastructure.Steam.Remote.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SteamAccountManager.Infrastructure.Steam.Remote.Dao
{
    public interface ISteamUserProvider
    {
        public Task<List<PlayerSummary>> GetSummariesAsync(params string[] steamIds);
        public Task<List<PlayerBans>> GetPlayerBansAsync(params string[] steamIds);
    }
}