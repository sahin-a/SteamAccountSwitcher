using System;
using Microsoft.Win32;
using SteamAccountManager.Domain.Steam.Exception;

namespace SteamAccountManager.Infrastructure.Steam.Local.Dao
{
    // TODO: create MacOs equivalent
    public class SteamWinRegistryConfig : ISteamConfig
    {
        private readonly RegistryKey _steamRegistryKey;

        public SteamWinRegistryConfig()
        {
            try
            {
                _steamRegistryKey = Registry.CurrentUser
                    .OpenSubKey("Software")?
                    .OpenSubKey("Valve")?
                    .OpenSubKey("Steam");
            }
            catch (Exception)
            {
                throw new SteamConfigNotFoundException();
            }
        }

        private string GetValue(string key)
        {
            var value = _steamRegistryKey?.GetValue(key);
            return value == null ? string.Empty : (string) value;
        }

        public string GetSteamExecutablePath()
        {
            return GetValue("SteamExe");
        }

        public string GetSteamPath()
        {
            return GetValue("SteamPath");
        }

        public bool SetAutoLoginUser(string accountName)
        {
            if (_steamRegistryKey == null)
                return false;

            try
            {
                _steamRegistryKey.SetValue("AutoLoginUser", accountName);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}