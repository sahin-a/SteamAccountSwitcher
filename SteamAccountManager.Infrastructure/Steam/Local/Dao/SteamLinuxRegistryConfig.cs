using Gameloop.Vdf;
using Gameloop.Vdf.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SteamAccountManager.Infrastructure.Steam.Local.Dao
{
    // TODO: Seperaten Reader erstellen und Unit Tests schreiben
    public class SteamLinuxRegistryConfig : ISteamConfig
    {
        private const string AutoLoginUser = "AutoLoginUser";
        private IEnumerable<VProperty> _steamRegistry;

        private void LoadRegistry()
        {
            var rootProperty = Gameloop.Vdf.VdfConvert.Deserialize(GetVdfContent());
            _steamRegistry = rootProperty["Registry"]["HKCU"]["Software"]["Valve"]["Steam"].Children<VProperty>();
        }

        private VProperty GetProperty(string key)
        {
            LoadRegistry();
            return _steamRegistry.First(property => property.Key == key);
        }

        private string GetVdfContent()
        {
            return File.ReadAllText(Path.Combine(GetSteamPath(), "registry.vdf"));
        }

        public string GetAutoLoginUser()
        {
            return GetProperty(AutoLoginUser).Value<string>();
        }

        public string GetSteamExecutablePath()
        {
            return "steam";
        }

        public string GetSteamPath()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".steam", "steam");
        }

        public void SetAutoLoginUser(string accountName)
        {
            var vdfContent = GetVdfContent();
            var regex = new Regex(pattern: "\"(AutoLoginUser)\"[ \t]*\"([A-z0-9]*)\"");

            var matches = regex.Matches(vdfContent);
        }
    }
}
