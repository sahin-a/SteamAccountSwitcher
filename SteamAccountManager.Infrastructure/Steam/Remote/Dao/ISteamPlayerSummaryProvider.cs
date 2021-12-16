using System.Collections.Generic;
using System.Threading.Tasks;
using SteamAccountManager.Infrastructure.Steam.Remote.Dto;

namespace SteamAccountManager.Infrastructure.Steam.Remote.Dao
{
    public interface ISteamPlayerSummaryProvider
    {
        public Task<List<PlayerSummary>> GetSummaryAsync(params string[] steamIds);
    }
}