using SteamAccountManager.Application.Steam.Model;
using SteamAccountManager.Application.Steam.Service;
using SteamAccountManager.Infrastructure.Steam.Exceptions;
using SteamAccountManager.Infrastructure.Steam.Remote.Dao;
using System.Threading.Tasks;

namespace SteamAccountManager.Infrastructure.Steam.Service
{
    public class SteamPlayerService : ISteamPlayerService
    {
        private readonly ISteamPlayerServiceProvider _playerServiceProvider;

        public SteamPlayerService(ISteamPlayerServiceProvider playerServiceProvider)
        {
            _playerServiceProvider = playerServiceProvider;
        }

        public async Task<SteamLevel> GetPlayerLevelAsync(string steamId)
        {
            try
            {
                var steamPlayerLevel = await _playerServiceProvider.GetPlayerLevelAsync(steamId);

                return new SteamLevel
                {
                    Level = steamPlayerLevel.PlayerLevel
                };
            }
            catch (RequestNotSuccessfulException)
            {
                throw new FailedToRetrieveSteamPlayerLevelException();
            }
        }
    }
}
