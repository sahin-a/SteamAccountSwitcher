using Newtonsoft.Json;

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
