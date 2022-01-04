using SteamAccountManager.Infrastructure.Steam.Remote.Dto;
using System.Threading.Tasks;

namespace SteamAccountManager.Infrastructure.Steam.Remote.Dao
{
    public interface ISteamPlayerServiceProvider
    {
        public Task<SteamPlayerLevel> GetPlayerLevelAsync(string steamId);
    }
}
