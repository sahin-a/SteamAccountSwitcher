using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamAccountManager.Infrastructure.Steam.Remote.Dto
{
    public class SteamPlayerLevel
    {
        [JsonProperty("player_level")]
        public int PlayerLevel { get; set; } = -1;
    }

    public class SteamPlayerLevelDto
    {
        [JsonProperty("response")]
        public SteamPlayerLevel Response { get; set; } = new SteamPlayerLevel();
    }
}
