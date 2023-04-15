namespace SteamAccountManager.Domain.Steam.Model
{
    public class Account
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string AvatarUrl { get; set; } = string.Empty;
        public string ProfileUrl { get; set; } = string.Empty;
        public bool IsVacBanned { get; set; }
        public bool IsCommunityBanned { get; set; }
        public bool IsLoginValid { get; set; }
        public DateTime LastLogin { get; set; }
        public int Level { get; set; }
        public bool IsLoggedIn { get; set; }
    }
}