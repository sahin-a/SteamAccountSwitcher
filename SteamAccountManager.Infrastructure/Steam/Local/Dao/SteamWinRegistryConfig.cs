using System;
using Microsoft.Win32;
using SteamAccountManager.Domain.Steam.Exception;

namespace SteamAccountManager.Infrastructure.Steam.Local.Dao
{
    // create MacOs and linux equivalent maybe maybe maybe
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
                    .OpenSubKey("Steam", true);
            }
            catch (Exception)
            {
                throw new SteamConfigNotFoundException();
            }
        }

        private string GetValue(string key)
        {
            var value = _steamRegistryKey?.GetValue(key);
            return value == null ? string.Empty : (string)value;
        }

        public string GetSteamExecutablePath()
        {
            return GetValue("SteamExe");
        }

        public string GetSteamPath()
        {
            return GetValue("SteamPath");
        }

        public void SetAutoLoginUser(string accountName)
        {
            try
            {
                _steamRegistryKey.SetValue("AutoLoginUser", accountName);
            }
            catch (Exception)
            {
                throw new UpdateAutoLoginUserFailedException();
            }
        }

        public string GetAutoLoginUser()
        {
            return GetValue("AutoLoginUser");
        }
    }
}