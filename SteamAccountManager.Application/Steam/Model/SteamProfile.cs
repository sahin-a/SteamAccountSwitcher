namespace SteamAccountManager.Application.Steam.Model
{
    public class SteamProfile
    {
        public string Id { get; set; } = string.Empty;
        public string Avatar { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public bool IsVacBanned { get; set; }
        public bool IsCommunityBanned { get; set; }
        public int Level { get; set; }
    }
}