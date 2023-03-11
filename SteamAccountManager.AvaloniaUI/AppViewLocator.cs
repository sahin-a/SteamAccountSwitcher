using ReactiveUI;
using SteamAccountManager.AvaloniaUI.ViewModels;
using SteamAccountManager.AvaloniaUI.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamAccountManager.AvaloniaUI
{
    public class AppViewLocator : IViewLocator
    {
        public IViewFor? ResolveView<T>(T viewModel, string? contract = null)
        {
            switch (viewModel)
            {
                case AccountSwitcherViewModel:
                    return new AccountSwitcher();
                default:
                    return null;
            }
        }
    }
}
