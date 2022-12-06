using SteamAccountManager.Application.Steam.Service;
using SteamAccountManager.WinUI.SAM.Steam.Model;
using System.Collections.ObjectModel;

namespace SteamAccountManager.WinUI.SAM.Steam.ViewModel
{
    internal class AccountSwitcherViewModel
    {
        private ISteamService _steamService;
        public ObservableCollection<Account> Accounts { get; set; }

        public AccountSwitcherViewModel(ISteamService steamService)
        {
            _steamService = steamService;
            Accounts = new ObservableCollection<Account>();

            LoadAccounts();
        }

        public async void LoadAccounts()
        {
            Accounts.Clear();

            var steamAccounts = await _steamService.GetAccounts();
            var accounts = steamAccounts.ConvertAll(steamAccount => new Account
            {
                SteamId = steamAccount.SteamId,
                Name = steamAccount.AccountName,
                ProfilePicture = steamAccount.ProfileUrl
            });

            accounts.ForEach(account => Accounts.Add(account));
        }

        public void OnAccountSelected(Account selectedAccount)
        {
            // can't write the registry from the winui application / it already runs in full trust mode :( no clue
            _steamService.SwitchAccount(selectedAccount.Name);
        }
    }
}
