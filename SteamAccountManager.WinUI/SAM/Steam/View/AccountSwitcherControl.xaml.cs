using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Autofac;
using SteamAccountManager.WinUI.SAM.Steam.ViewModel;
using SteamAccountManager.WinUI.SAM.Steam.Model;

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
