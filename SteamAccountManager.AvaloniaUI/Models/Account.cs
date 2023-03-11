using Avalonia.Media.Imaging;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SteamAccountManager.AvaloniaUI.Models
{
    public class Account : INotifyPropertyChanged
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
        public Rank Rank { get; set; } = new Rank()
        {
            Level = -1
        };
        public bool ShowLevel
        {
            get { return Rank.Level >= 0; }
        }


#pragma warning disable CS8612 // Die NULL-Zulässigkeit von Verweistypen im Typ entspricht nicht dem implizit implementierten Member.
        public event PropertyChangedEventHandler PropertyChanged;
#pragma warning restore CS8612 // Die NULL-Zulässigkeit von Verweistypen im Typ entspricht nicht dem implizit implementierten Member.

        // This method is called by the Set accessor of each property.
        // The CallerMemberName attribute that is applied to the optional propertyName
        // parameter causes the property name of the caller to be substituted as an argument.
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
