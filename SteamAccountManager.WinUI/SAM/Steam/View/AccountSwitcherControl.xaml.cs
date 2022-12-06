using Autofac;
using Microsoft.UI.Xaml.Controls;
using SteamAccountManager.WinUI.SAM.Steam.Model;
using SteamAccountManager.WinUI.SAM.Steam.ViewModel;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace SteamAccountManager.WinUI.SAM.Steam.View
{
    public sealed partial class AccountSwitcherControl : UserControl
    {
        private AccountSwitcherViewModel _viewModel;

        public AccountSwitcherControl()
        {
            this.InitializeComponent();
            _viewModel = Dependencies.Container.Resolve<AccountSwitcherViewModel>();
            DataContext = _viewModel;
        }

        private void AccountSelection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _viewModel.OnAccountSelected((sender as ListView).SelectedItem as Account);
        }
    }
}
