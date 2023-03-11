using Autofac;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using SteamAccountManager.AvaloniaUI.Models;
using SteamAccountManager.AvaloniaUI.ViewModels;

namespace SteamAccountManager.AvaloniaUI.Views
{
    public partial class AccountSwitcherView : ReactiveUserControl<AccountSwitcherViewModel>
    {
        public AccountSwitcherView()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void AccountSelection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ListBox)?.SelectedItem is Account selectedAccount)
            {
                ViewModel?.OnAccountSelected(selectedAccount);
            }
        }
    }
}
