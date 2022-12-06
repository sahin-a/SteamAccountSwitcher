using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SteamAccountManager.Infrastructure.Steam.Remote.Dto
{
    public class SteamPlayerSummariesDto
    {
        [JsonProperty("response")] public SteamPlayerSummariesResponse Response { get; set; }
    }

    public class SteamPlayerSummariesResponse
    {
        [JsonProperty("players")] public List<PlayerSummary> PlayerSummaries { get; set; }
    }

    public class PlayerSummary
    {
        [JsonProperty("steamid")] public string SteamId { get; set; }

        [JsonProperty("personaname")] public string PersonaName { get; set; }

        [JsonProperty("profileurl")] public Uri ProfileUrl { get; set; }

        [JsonProperty("avatarfull")] public Uri Avatar { get; set; }

        [JsonProperty("lastlogoff")] public long LastLogOff { get; set; } = -1L;

        [JsonProperty("timecreated")] public long TimeCreated { get; set; } = -1L;
    }
}