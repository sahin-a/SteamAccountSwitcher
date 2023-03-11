using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using SteamAccountManager.AvaloniaUI.ViewModels;

namespace SteamAccountManager.AvaloniaUI.Views
{
    public partial class SettingsView : ReactiveUserControl<SettingsViewModel>
    {
        public SettingsView()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
