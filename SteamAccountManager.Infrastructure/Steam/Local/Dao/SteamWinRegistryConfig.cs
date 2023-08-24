using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using Microsoft.Win32;
using SteamAccountManager.Domain.Steam.Exceptions;

namespace SteamAccountManager.Infrastructure.Steam.Local.Dao
{
    // create MacOs and linux equivalent maybe maybe maybe
    public class SteamWinRegistryConfig : ISteamConfig
    {
        private readonly RegistryKey _steamRegistryKey;

        [SuppressMessage("Interoperability", "CA1416:Plattformkompatibilität überprüfen",
            Justification = "<Ausstehend>")]
        public SteamWinRegistryConfig()
        {
            try
            {
                _steamRegistryKey = Registry.CurrentUser
                    .OpenSubKey("Software")?
                    .OpenSubKey("Valve")?
                    .OpenSubKey("Steam", true);
            }
            catch (Exception)
            {
                throw new SteamConfigNotFoundException();
            }
        }

        [SuppressMessage("Interoperability", "CA1416:Plattformkompatibilität überprüfen",
            Justification = "<Ausstehend>")]
        private string? GetValue(string key)
        {
            var value = _steamRegistryKey?.GetValue(key);
            return (string?)value;
        }

        public string GetSteamExecutablePath()
        {
            return GetValue("SteamExe") ?? string.Empty;
        }

        public string GetSteamPath()
        {
            return GetValue("SteamPath") ?? string.Empty;
        }

        [SuppressMessage("Interoperability", "CA1416:Plattformkompatibilität überprüfen",
            Justification = "<Ausstehend>")]
        public void SetAutoLoginUser(string accountName)
        {
            try
            {
                _steamRegistryKey.SetValue("AutoLoginUser", accountName);
                _steamRegistryKey.SetValue("RememberPassword", 1);
            }
            catch (Exception)
            {
                throw new UpdateAutoLoginUserFailedException();
            }
        }

        public string? GetAutoLoginUser()
        {
            return GetValue("AutoLoginUser");
        }

        public string GetLoginUsersVdfPath()
        {
            return Path.Combine(
                GetSteamPath(),
                "config",
                "loginusers.vdf"
            );
        }
    }
}