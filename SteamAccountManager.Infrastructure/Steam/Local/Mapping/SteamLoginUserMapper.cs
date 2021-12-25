using System;
using System.Collections.Generic;
using SteamAccountManager.Application.Steam.Model;
using SteamAccountManager.Infrastructure.Steam.Local.Dto;

namespace SteamAccountManager.Infrastructure.Steam.Local.Mapping
{
    public static class SteamLoginUserMapper
    {
        /* LoginUserDto <=> SteamLoginUser */
        public static SteamLoginUser ToSteamLoginUser(this LoginUserDto dto)
        {
            var lastLogin = DateTimeOffset.FromUnixTimeSeconds(long.Parse(dto.Timestamp));

            return new SteamLoginUser.Builder()
                .SetSteamId(dto.SteamId)
                .SetAccountName(dto.AccountName)
                .SetIsLoginTokenValid(dto.PasswordRemembered)
                .SetLastLogin(lastLogin.UtcDateTime)
                .Build();
        }

        public static List<SteamLoginUser> ToSteamLoginUsers(this List<LoginUserDto> dtos)
        {
            return dtos.ConvertAll(dto => dto.ToSteamLoginUser());
        }
    }
}