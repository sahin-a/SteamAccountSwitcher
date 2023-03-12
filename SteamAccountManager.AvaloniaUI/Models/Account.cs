using System;
using Avalonia.Media.Imaging;

namespace SteamAccountManager.AvaloniaUI.Models
{
    public class Account
    {
        public IBitmap? ProfilePicture { get; set; }
        public Uri? ProfilePictureUrl { get; set; }
        public string SteamId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string ProfileUrl { get; set; } = string.Empty;
        public bool IsVacBanned { get; set; }
        public bool IsCommunityBanned { get; set; }
        public string LastLogin { get; set; } = string.Empty;

        public Rank Rank { get; set; } = new()
        {
            Level = -1
        };

        public bool ShowLevel
        {
            get { return Rank.Level >= 0; }
        }
    }
}