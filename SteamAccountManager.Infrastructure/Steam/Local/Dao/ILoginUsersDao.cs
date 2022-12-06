using System.Collections.Generic;
using System.Threading.Tasks;
using SteamAccountManager.Infrastructure.Steam.Local.Dto;

namespace SteamAccountManager.Infrastructure.Steam.Local.Dao
{
    public interface ILoginUsersDao
    {
        public Task<List<LoginUserDto>> GetLoggedUsers();
    }
}