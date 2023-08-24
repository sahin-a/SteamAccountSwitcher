using System;
using Avalonia.Media.Imaging;
using ReactiveUI;

namespace SteamAccountManager.AvaloniaUI.Models
{
    public class Account : ReactiveObject
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
        public bool IsLoggedIn { get; set; }
        private bool _isBlacklisted;

        public bool IsBlacklisted
        {
            get => _isBlacklisted;
            set => this.RaiseAndSetIfChanged(ref _isBlacklisted, value);
        }

        private bool _isBlacklistToggleVisible;

        public bool IsBlacklistToggleVisible
        {
            get => _isBlacklistToggleVisible;
            set => this.RaiseAndSetIfChanged(ref _isBlacklistToggleVisible, value);
        }

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