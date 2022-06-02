using SteamAccountManager.Application.Steam.Service;
using SteamAccountManager.AvaloniaUI.Mappers;
using SteamAccountManager.AvaloniaUI.Models;
using SteamAccountManager.AvaloniaUI.ViewModels.Commands;
using SteamAccountManager.Domain.Steam.Observables;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SteamAccountManager.AvaloniaUI.ViewModels
{
    // TODO: looks ridicilous, I should refactor all of this but I don't feel bored enough yet
    internal class AccountSwitcherViewModel
    {
        private ISteamService _steamService;
        private AccountMapper _accountMapper;

        public ObservableCollection<Account> Accounts { get; }
        public ICommand ProfileClickedCommand { get; }
        public ICommand RefreshAccountsCommand { get; }
        public ICommand ShowInfoCommand { get; }
        public ICommand AddAccountCommand { get; }


        public AccountSwitcherViewModel(ISteamService steamService, AccountMapper accountMapper, IAccountStorageObservable accountStorageObserver)
        {
            _steamService = steamService;
            _accountMapper = accountMapper;

            Accounts = new ObservableCollection<Account>();
            ProfileClickedCommand = new ProfileClickedCommand();
            RefreshAccountsCommand = new QuickCommand(LoadAccounts);
            ShowInfoCommand = new QuickCommand(ShowInfo);
            AddAccountCommand = new QuickCommand(AddAccount);
            accountStorageObserver.Subscribe(Accounts_Changed);

            LoadAccounts();
        }

        private void Accounts_Changed(List<Domain.Steam.Model.Account>? accounts)
        {
            LoadAccounts();
        }

        private void AddAccount()
        {
            _steamService.SwitchAccount(string.Empty);
        }

        private IOrderedEnumerable<Account> SortAccounts(Account[] accounts) => accounts.OrderByDescending(x => x.Rank.Level)
                .ThenBy(x => x.Name)
                .ThenBy(x => x.IsVacBanned);

        private void UpdateEntries(IEnumerable<Account> accounts)
        {
            foreach (var account in accounts)
            {
                var currentAccount = Accounts.Where(x => x.SteamId == account.SteamId)
                    .FirstOrDefault(defaultValue: null);

                if (currentAccount is not null)
                {
                    var index = Accounts.IndexOf(currentAccount);
                    Accounts[index] = account;
                    continue;
                }
                Accounts.Add(account);
            }
        }

        public async void LoadAccounts()
        {
            var steamAccounts = await _steamService.GetAccounts();
            var accounts = await Task.WhenAll(steamAccounts.ConvertAll(x => _accountMapper.FromSteamAccount(x)));
            UpdateEntries(SortAccounts(accounts));
        }

        public void OnAccountSelected(Account selectedAccount)
        {
            _steamService.SwitchAccount(selectedAccount.Name);
        }

        public void ShowInfo()
        {
            System.Diagnostics.Process.Start("explorer", "https://github.com/sahin-a/SteamAccountManager/");
        }
    }
}
