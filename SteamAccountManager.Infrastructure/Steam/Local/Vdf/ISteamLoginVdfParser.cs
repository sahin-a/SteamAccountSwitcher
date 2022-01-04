using SteamAccountManager.Infrastructure.Steam.Local.Dto;
using System.Collections.Generic;

namespace SteamAccountManager.Infrastructure.Steam.Local.Vdf
{
    public interface ISteamLoginVdfParser
    {
        public List<LoginUserDto> ParseLoginUsers(string vdfContent);
    }
}