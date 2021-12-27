using SteamAccountManager.Infrastructure.Steam.Remote.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamAccountManager.Infrastructure.Steam.Remote.Dao
{
    public interface ISteamPlayerServiceProvider
    {
        public Task<SteamPlayerLevel> GetPlayerLevelAsync(string steamId);
    }
}
