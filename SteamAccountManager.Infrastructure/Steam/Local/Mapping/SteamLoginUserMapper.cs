using System.Collections.Generic;
using SteamAccountManager.Domain.Steam.Local.POCO;
using SteamAccountManager.Infrastructure.Steam.Local.Dto;

namespace SteamAccountManager.Infrastructure.Steam.Local.Mapping
{
    public static class SteamLoginUserMapper
    {
        /* LoginUserDto <=> SteamLoginUser */
        public static SteamLoginUser ToSteamLoginUser(this LoginUserDto dto)
        {
            return new SteamLoginUser.Builder()
                .SetSteamId(dto.SteamId)
                .SetAccountName(dto.AccountName)
                .SetIsLoginTokenValid(dto.PasswordRemembered)
                .Build();
        }

        public static List<SteamLoginUser> ToSteamLoginUsers(this List<LoginUserDto> dtos)
        {
            return dtos.ConvertAll(dto => dto.ToSteamLoginUser());
        }
    }
}