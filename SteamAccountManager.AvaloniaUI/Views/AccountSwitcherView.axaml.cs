using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using SteamAccountManager.AvaloniaUI.ViewModels;

namespace SteamAccountManager.AvaloniaUI.Views
{
    public partial class AccountSwitcherView : ReactiveUserControl<AccountSwitcherViewModel>
    {
        public AccountSwitcherView()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}