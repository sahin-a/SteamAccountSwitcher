using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using SteamAccountManager.AvaloniaUI.ViewModels;

namespace SteamAccountManager.AvaloniaUI.Views
{
    public partial class MainWindowView : ReactiveWindow<MainWindowViewModel>
    {
        public MainWindowView()
        {
            AvaloniaXamlLoader.Load(this);
#if DEBUG
            this.AttachDevTools();
#endif
        }
    }
}