﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using Gameloop.Vdf;
using Gameloop.Vdf.Linq;
using SteamAccountManager.Infrastructure.Steam.Local.Dto;

namespace SteamAccountManager.Infrastructure.Steam.Local.Vdf
{
    public class SteamLoginVdfParser : ISteamLoginVdfParser
    {
        private static class AccountKeys
        {
            public const string AccountName = "AccountName";
            public const string PersonaName = "PersonaName";
            public const string RememberPassword = "RememberPassword";
            public const string MostRecent = "MostRecent";
            public const string Timestamp = "Timestamp";
        }
        
        private void SetDtoProperty(LoginUserDto dto, VProperty vProperty)
        {
            string value = vProperty.Value.ToString();
                    
            switch (vProperty.Key)
            {
                case AccountKeys.AccountName:
                    dto.AccountName = value;
                    break;
                case AccountKeys.PersonaName:
                    dto.PersonaName = value;
                    break;
                case AccountKeys.RememberPassword:
                    dto.PasswordRemembered = Int32.Parse(value) > 0;
                    break;
                case AccountKeys.MostRecent:
                    dto.MostRecent = Int32.Parse(value) > 0;
                    break;
                case AccountKeys.Timestamp:
                    dto.Timestamp = value;
                    break;
            }
        }
        
        public List<LoginUserDto> ParseLoginUsers(string vdfContent)
        {
            List<LoginUserDto> loginUsersDto = new();
            
            var vRootProperty = VdfConvert.Deserialize(vdfContent);
            
            foreach (var vProperty in vRootProperty.Value.Children<VProperty>())
            {
                var steamId = vProperty.Key;
                
                LoginUserDto dto = new LoginUserDto()
                {
                    SteamId = steamId
                };

                Debug.WriteLine($"SteamId: {steamId}");

                foreach (var vChildProperty in vProperty.Value.Children<VProperty>())
                {
                    SetDtoProperty(dto, vChildProperty);
                    Debug.WriteLine($"{vChildProperty.Key}: {vChildProperty.Value}");
                }
                
                loginUsersDto.Add(dto);
            }

            return loginUsersDto;
        }
    }
}