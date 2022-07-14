using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace SteamAccountManager.AvaloniaUI.Views
{
    public partial class LocalNotification : Window
    {
        public LocalNotification()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
