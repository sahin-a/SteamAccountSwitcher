using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Avalonia.Media.Imaging;

namespace SteamAccountManager.AvaloniaUI.Models
{
    internal class Account : INotifyPropertyChanged
    {
        public IBitmap ProfilePicture { get; set; }
        public string SteamId { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string ProfileUrl { get; set; }
        public bool IsVacBanned { get; set; }
        public bool IsCommunityBanned { get; set; }
        public DateTime LastLogin { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

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
