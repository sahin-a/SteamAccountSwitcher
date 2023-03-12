using SteamAccountManager.Infrastructure.Steam.Local.Dto;
using System;
using System.Collections.Generic;
using SteamAccountManager.Domain.Steam.Model;

namespace SteamAccountManager.Infrastructure.Steam.Local.Mapping
{
    public static class SteamLoginUserMapper
    {
        /* LoginUserDto <=> SteamLoginUser */
        public static LoginUser ToSteamLoginUser(this LoginUserDto dto)
        {
            var lastLogin = DateTimeOffset.FromUnixTimeSeconds(long.Parse(dto.Timestamp));

            return new LoginUser.Builder()
                .SetSteamId(dto.SteamId)
                .SetAccountName(dto.AccountName)
                .SetUsername(dto.PersonaName)
                .SetIsLoginTokenValid(dto.PasswordRemembered)
                .SetLastLogin(lastLogin.UtcDateTime)
                .Build();
        }

        public static List<LoginUser> ToSteamLoginUsers(this List<LoginUserDto> dtos)
        {
            return dtos.ConvertAll(dto => dto.ToSteamLoginUser());
        }
    }
}