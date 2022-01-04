using SteamAccountManager.Infrastructure.Steam.Local.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SteamAccountManager.Infrastructure.Steam.Local.Dao
{
    public interface ILoginUsersDao
    {
        public Task<List<LoginUserDto>> GetLoggedUsers();
    }
}