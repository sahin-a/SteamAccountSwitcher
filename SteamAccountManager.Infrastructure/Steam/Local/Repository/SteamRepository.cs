using System.Collections.Generic;
using System.Linq;
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
        private List<SteamLoginUser> _steamLoginUsers;
        
        public SteamRepository(ILocalSteamDataSource steamDataSource)
        {
            _steamDataSource = steamDataSource;
        }
        
        public async Task<List<SteamLoginUser>> GetSteamLoginUsers()
        {
            _steamLoginUsers = (await _steamDataSource.GetLoggedInUsers())
                .ToSteamLoginUsers();

            return _steamLoginUsers;
        }

        public void UpdateAutoLoginUser(SteamLoginUser steamLoginUser)
        {
            _steamDataSource.UpdateAutoLoginUser(steamLoginUser.AccountName);
        }

        public SteamLoginUser GetCurrentAutoLoginUser()
        {
            // TODO: fix this; if GetSteamLoginUsers never gets called, the list will be uninitialized so there will be no finding
            string currentUser = _steamDataSource.GetCurrentAutoLoginUser();
            // TODO: throw exception if user not found
            return _steamLoginUsers.FirstOrDefault(user => user.AccountName == currentUser);
        }
    }
}