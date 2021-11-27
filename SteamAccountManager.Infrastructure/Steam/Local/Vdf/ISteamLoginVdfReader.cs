using System.Collections.Generic;
using System.Threading.Tasks;
using SteamAccountManager.Infrastructure.Steam.Local.Dto;

namespace SteamAccountManager.Infrastructure.Steam.Local.Vdf
{
    public interface ISteamLoginVdfReader
    {
        public Task<List<LoginUserDto>> GetParsedLoginUsersVdf();
    }
}