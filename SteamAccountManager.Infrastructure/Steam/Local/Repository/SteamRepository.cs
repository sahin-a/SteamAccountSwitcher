using System.Collections.Generic;
using System.Threading.Tasks;
using SteamAccountManager.Domain.Steam.Local.POCO;
using SteamAccountManager.Domain.Steam.Local.Repository;
using SteamAccountManager.Infrastructure.Steam.Local.DataSource;
using SteamAccountManager.Infrastructure.Steam.Local.Mapping;

namespace SteamAccountManager.Infrastructure.Steam.Local.Repository
{
    public class SteamRepository : ISteamRepository
    {
        private readonly ILocalSteamDataSource _steamDataSource;

        public SteamRepository(ILocalSteamDataSource steamDataSource)
        {
            _steamDataSource = steamDataSource;
        }
        
        public async Task<List<SteamLoginUser>> GetSteamLoginUsers()
        {
            return (await _steamDataSource.GetLoggedInUsers())
                .ToSteamLoginUsers();
        }

        public bool UpdateAutoLoginUser(string steamId)
        {
            // TODO: stopped here
            return _steamDataSource.UpdateAutoLoginUser(steamId);
        }
    }
}