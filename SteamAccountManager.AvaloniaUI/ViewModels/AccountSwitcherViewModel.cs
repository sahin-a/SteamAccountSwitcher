using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using ReactiveUI;
using SteamAccountManager.AvaloniaUI.Common;
using SteamAccountManager.AvaloniaUI.Mappers;
using SteamAccountManager.AvaloniaUI.Models;
using SteamAccountManager.AvaloniaUI.ViewModels.Commands;
using SteamAccountManager.Domain.Steam.Configuration.Model;
using SteamAccountManager.Domain.Steam.Observables;
using SteamAccountManager.Domain.Steam.Service;
using SteamAccountManager.Domain.Steam.Storage;
using SteamAccountManager.Domain.Steam.UseCase;

namespace SteamAccountManager.AvaloniaUI.ViewModels
{
    // TODO: looks ridicilous, I should refactor all of this but I don't feel bored enough yet
    // TOOD: switch to reactive commands etc.
    public class AccountSwitcherViewModel : RoutableViewModel
    {
        private readonly IGetAccountsWithDetailsUseCase _getAccountsUseCase;
        private readonly ISwitchAccountUseCase _switchAccountUseCase;
        private readonly AccountMapper _accountMapper;
        private readonly ILocalNotificationService _notificationService;
        private readonly IPrivacyConfigStorage _privacyConfigStorage;

        public AdvancedObservableCollection<Account> Accounts { get; private set; }
        public ICommand ProfileClickedCommand { get; }
        public ICommand RefreshAccountsCommand { get; }
        public ICommand ShowInfoCommand { get; }
        public ICommand AddAccountCommand { get; }
        public Account? SelectedAccount { get; set; }
        public VisibilityConfig Config { get; private set; } = new();

        public AccountSwitcherViewModel
        (
            IScreen screen,
            IGetAccountsWithDetailsUseCase getAccountsUseCase,
            ISwitchAccountUseCase switchAccountUseCase,
            AccountMapper accountMapper,
            IAccountStorageObservable accountStorageObserver,
            ILocalNotificationService notificationService,
            IPrivacyConfigStorage privacyConfigStorage
        ) : base(screen)
        {
            _getAccountsUseCase = getAccountsUseCase;
            _switchAccountUseCase = switchAccountUseCase;
            _accountMapper = accountMapper;
            _notificationService = notificationService;
            _privacyConfigStorage = privacyConfigStorage;

            Accounts = new AdvancedObservableCollection<Account>();
            ProfileClickedCommand = new ProfileClickedCommand();
            RefreshAccountsCommand = new QuickCommand(LoadAccounts);
            ShowInfoCommand = new QuickCommand(ShowInfo);
            AddAccountCommand = new QuickCommand(AddAccount);
            accountStorageObserver.Subscribe(Accounts_Changed);

            InitializeVisibilityConfig();
            LoadAccounts();
        }

        private void InitializeVisibilityConfig()
        {
            var config = _privacyConfigStorage.Get()?.DetailSettings;
            if (config is null)
                return;

            foreach (var setting in config)
            {
                switch (setting.DetailType)
                {
                    case AccountDetailType.LoginName:
                        Config.ShowLoginName = setting.IsEnabled;
                        break;
                    case AccountDetailType.Level:
                        Config.ShowLevel = setting.IsEnabled;
                        break;
                    case AccountDetailType.Avatar:
                        Config.ShowAvatar = setting.IsEnabled;
                        break;
                    case AccountDetailType.BanStatus:
                        Config.ShowBanStatus = setting.IsEnabled;
                        break;
                }
            }
        }

        private void Accounts_Changed(List<Domain.Steam.Model.Account>? accounts)
        {
            LoadAccounts();
        }

        private async void AddAccount()
        {
            await _switchAccountUseCase.Execute(string.Empty);
        }

        private IEnumerable<Account> SortAccounts(Account[] accounts) => accounts.OrderByDescending(x => x.Rank.Level)
            .ThenBy(x => x.Name)
            .ThenBy(x => x.IsVacBanned);

        private async Task<IEnumerable<Account>> GetAccounts()
        {
            var steamAccounts = await _getAccountsUseCase.Execute();
            var accounts = await Task.WhenAll(steamAccounts.ConvertAll(x => _accountMapper.FromSteamAccount(x)));
            return SortAccounts(accounts);
        }

        public async void LoadAccounts()
        {
            InitializeVisibilityConfig();
            Accounts.SetItems((await GetAccounts()).ToList());
        }

        public async void OnAccountSelected(Account selectedAccount)
        {
            await _switchAccountUseCase.Execute(selectedAccount.Name);
            SelectedAccount = selectedAccount;
            _notificationService.Send
            (
                new Notification
                (
                    selectedAccount.Name,
                    selectedAccount.Username,
                    selectedAccount.ProfilePictureUrl
                )
            );
        }

        public void ShowInfo()
        {
            if (OperatingSystem.IsWindows())
            {
                Process.Start("explorer", "https://github.com/sahin-a/SteamAccountManager/");
            }
            else
            {
                Process.Start("xdg-open", "https://github.com/sahin-a/SteamAccountManager/");
            }
        }
    }

    public class VisibilityConfig
    {
        public bool ShowUsername { get; set; } = true;
        public bool ShowLoginName { get; set; } = true;
        public bool ShowAvatar { get; set; } = true;
        public bool ShowLevel { get; set; } = true;
        public bool ShowBanStatus { get; set; } = true;
    }
}