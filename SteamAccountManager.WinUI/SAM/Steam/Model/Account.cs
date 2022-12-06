using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SteamAccountManager.WinUI.SAM.Steam.Model
{
    internal class Account : INotifyPropertyChanged
    {
        public string ProfilePicture { get; set; }
        public string SteamId { get; set; }
        public string Name { get; set; }

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
