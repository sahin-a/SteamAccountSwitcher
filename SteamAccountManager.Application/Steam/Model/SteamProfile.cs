namespace SteamAccountManager.Application.Steam.Model
{
    public class SteamProfile
    {
        public string Id { get; set; }
        public string Avatar { get; set; }
        public string Username { get; set; }
        public string Url { get; set; }
        public bool IsVacBanned { get; set; }
        public bool IsCommunityBanned { get; set; }
    }
}