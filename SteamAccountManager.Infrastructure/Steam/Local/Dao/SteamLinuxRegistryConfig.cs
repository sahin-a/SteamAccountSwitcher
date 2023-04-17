using Gameloop.Vdf.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Gameloop.Vdf;

namespace SteamAccountManager.Infrastructure.Steam.Local.Dao
{
    public class SteamLinuxRegistryConfig : ISteamConfig
    {
        private const string AutoLoginUser = "AutoLoginUser";

        private VToken? SteamRegistry
        {
            get
            {
                var rootProperty = VdfConvert.Deserialize(GetVdfContent());
                return rootProperty.Value["HKCU"]?["Software"]?["Valve"]?["Steam"];
            }
        }

        private string GetVdfContent()
        {
            return File.ReadAllText(Path.Combine(GetSteamPath(), "registry.vdf"));
        }

        public string GetAutoLoginUser()
        {
            return SteamRegistry[AutoLoginUser].Value<string>();
        }

        public string GetSteamExecutablePath()
        {
            return "steam";
        }

        public string GetSteamPath()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".steam");
        }

        public void SetAutoLoginUser(string accountName)
        {
            var vdfContent = GetVdfContent();
            var regex = new Regex(pattern: "\"(AutoLoginUser)\"[ \t]*\"([A-z0-9]*)\"");

            var matches = regex.Matches(vdfContent);
        }

        public string GetLoginUsersVdfPath()
        {
            return Path.Combine(
                GetSteamPath(),
                "steam",
                "config",
                "loginusers.vdf"
            );
        }
    }
}
