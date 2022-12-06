using System.Collections.Generic;
using SteamAccountManager.Infrastructure.Steam.Local.Dto;

namespace SteamAccountManager.Infrastructure.Steam.Local.Vdf
{
    public interface ISteamLoginVdfParser
    {
        public List<LoginUserDto> ParseLoginUsers(string vdfContent);
    }
}