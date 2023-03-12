using SteamAccountManager.Infrastructure.Steam.Remote.Dao;
using System.Threading.Tasks;
using SteamAccountManager.Domain.Steam.Service;
using SteamAccountManager.Infrastructure.Steam.Exceptions;

namespace SteamAccountManager.Infrastructure.Steam.Service
{
    public class SteamPlayerService : ISteamPlayerService
    {
        private readonly ISteamPlayerServiceProvider _playerServiceProvider;

        public SteamPlayerService(ISteamPlayerServiceProvider playerServiceProvider)
        {
            _playerServiceProvider = playerServiceProvider;
        }

        public async Task<int> GetPlayerLevelAsync(string steamId)
        {
            try
            {
                var steamPlayerLevel = await _playerServiceProvider.GetPlayerLevelAsync(steamId);

                return steamPlayerLevel.PlayerLevel;
            }
            catch (RequestNotSuccessfulException)
            {
                throw new FailedToRetrieveSteamPlayerLevelException();
            }
        }
    }
}
