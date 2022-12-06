using System.Collections.Generic;

namespace SteamAccountManager.Infrastructure.Steam.Remote.Dto
{
    public class SteamPlayerBansDto
    {
        public List<PlayerBans> Players { get; set; }
    }

    public class PlayerBans
    {
        public string SteamId { get; set; }
        public bool CommunityBanned { get; set; }
        public bool VacBanned { get; set; }
        public long NumberOfVacBans { get; set; }
        public long DaysSinceLastBan { get; set; }
        public long NumberOfGameBans { get; set; }
        public string EconomyBan { get; set; }
    }
}