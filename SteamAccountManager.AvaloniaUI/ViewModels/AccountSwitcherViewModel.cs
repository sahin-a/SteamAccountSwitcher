using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using ReactiveUI;
using SteamAccountManager.AvaloniaUI.Common;
using SteamAccountManager.AvaloniaUI.Mappers;
using SteamAccountManager.AvaloniaUI.Models;
using SteamAccountManager.AvaloniaUI.Services;
using SteamAccountManager.AvaloniaUI.ViewModels.Commands;
using SteamAccountManager.Domain.Common.EventSystem;
using SteamAccountManager.Domain.Steam.Blacklisting.Model;
using SteamAccountManager.Domain.Steam.Blacklisting.Storage;
using SteamAccountManager.Domain.Steam.Configuration.Model;
using SteamAccountManager.Domain.Steam.Service;
using SteamAccountManager.Domain.Steam.Storage;
using SteamAccountManager.Domain.Steam.UseCase;

namespace SteamAccountManager.AvaloniaUI.ViewModels
{
    public class AccountSwitcherViewModel : RoutableViewModel
    {
        private readonly IGetAccountsWithDetailsUseCase _getAccountsUseCase;
        private readonly ISwitchAccountUseCase _switchAccountUseCase;
        private readonly AccountMapper _accountMapper;
        private readonly ILocalNotificationService _notificationService;
        private readonly IPrivacyConfigStorage _privacyConfigStorage;
        private readonly EventBus _eventBus;
        private readonly InfoService _infoService;
        private readonly INotificationConfigStorage _notificationConfigStorage;
        private readonly IBlacklistedAccountsStorage _blacklistedAccountsStorage;

        public List<Account> AllAccounts { get; private set; } = new();
        public List<Account> WhitelistedAccounts { get; private set; } = new();
        public AdvancedObservableCollection<Account> AccountsForDisplay { get; private set; }
        public ICommand AccountSelectedCommand { get; set; }
        public ICommand ProfileClickedCommand { get; }
        public ICommand RefreshAccountsCommand { get; }
        public ICommand ShowInfoCommand { get; }
        public ICommand AddAccountCommand { get; }
        public ICommand BlacklistAccountCommand { get; set; }
        public ICommand ToggleBlacklistingModeCommand { get; set; }
        private bool _isBlacklistToggleVisible;

        public bool IsBlacklistToggleVisible
        {
            get => _isBlacklistToggleVisible;
            set => this.RaiseAndSetIfChanged(ref _isBlacklistToggleVisible, value);
        }

        public VisibilityConfig Config { get; private set; } = new();

        private bool _isLoading;

        public bool IsLoading
        {
            get => _isLoading;
            set => this.RaiseAndSetIfChanged(ref _isLoading, value);
        }

        public AccountSwitcherViewModel
        (
            IScreen screen,
            IGetAccountsWithDetailsUseCase getAccountsUseCase,
            ISwitchAccountUseCase switchAccountUseCase,
            AccountMapper accountMapper,
            ILocalNotificationService notificationService,
            IPrivacyConfigStorage privacyConfigStorage,
            INotificationConfigStorage notificationConfigStorage,
            IBlacklistedAccountsStorage blacklistedAccountsStorage,
            EventBus eventBus,
            InfoService infoService
        ) : base(screen)
        {
            _getAccountsUseCase = getAccountsUseCase;
            _switchAccountUseCase = switchAccountUseCase;
            _accountMapper = accountMapper;
            _notificationService = notificationService;
            _privacyConfigStorage = privacyConfigStorage;
            _notificationConfigStorage = notificationConfigStorage;
            _blacklistedAccountsStorage = blacklistedAccountsStorage;
            _eventBus = eventBus;
            _infoService = infoService;

            AccountsForDisplay = new AdvancedObservableCollection<Account>();
            ProfileClickedCommand = new ProfileClickedCommand();
            RefreshAccountsCommand = new QuickCommand(LoadAccounts);
            ShowInfoCommand = new QuickCommand(ShowInfo);
            AddAccountCommand = new QuickCommand(AddAccount);
            AccountSelectedCommand = ReactiveCommand.Create((Account account) => OnAccountSelected(account));
            BlacklistAccountCommand =
                ReactiveCommand.Create((Account account) => ToggleBlacklistingForAccount(account));
            ToggleBlacklistingModeCommand = ReactiveCommand.Create(ToggleBlacklistingMode);

            SubscribeToEventBus();
            LoadVisibilityConfig();
            LoadAccounts();
        }

        private void SubscribeToEventBus()
        {
            _eventBus.Subscribe(subscriberKey: GetType().Name, Events.ACCOUNTS_UPDATED,
                _ => LoadAccounts()
            );
            _eventBus.Subscribe(subscriberKey: GetType().Name, Events.PRIVACY_CONFIG_UPDATED,
                _ => LoadVisibilityConfig()
            );
        }

        private async void LoadVisibilityConfig()
        {
            var config = (await _privacyConfigStorage.Get())?.DetailSettings;
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

        private void DisplayAccountsForCurrentState()
        {
            AccountsForDisplay.SetItems(IsBlacklistToggleVisible
                ? SortAccountsForManagement(AllAccounts)
                : SortAccounts(WhitelistedAccounts));
        }

        private Task ToggleBlacklistingMode() => Task.Run(() =>
        {
            IsBlacklistToggleVisible = !IsBlacklistToggleVisible;
            DisplayAccountsForCurrentState();
        });

        private async Task WhitelistAccount(Account account, AccountBlacklist blacklist)
        {
            blacklist.BlacklistedIds.Remove(account.SteamId);
            account.IsBlacklisted = false;
            WhitelistedAccounts.Add(account);
            await _blacklistedAccountsStorage.Set(blacklist);
        }

        private async Task BlacklistAccount(Account account, AccountBlacklist blacklist)
        {
            blacklist.BlacklistedIds.Add(account.SteamId);
            account.IsBlacklisted = true;
            WhitelistedAccounts.Remove(account);
            await _blacklistedAccountsStorage.Set(blacklist);
        }

        private async Task ToggleBlacklistingForAccount(Account account)
        {
            var blacklist = await _blacklistedAccountsStorage.Get(new());

            if (blacklist.BlacklistedIds.Contains(account.SteamId))
                await WhitelistAccount(account, blacklist);
            else
                await BlacklistAccount(account, blacklist);
        }

        private async void AddAccount()
        {
            await _switchAccountUseCase.Execute(string.Empty);
        }

        private List<Account> SortAccountsForManagement(List<Account> accounts) => accounts
            .OrderBy(x => x.IsBlacklisted)
            .ToList();

        private List<Account> SortAccounts(List<Account> accounts) => accounts
            .OrderByDescending(x => x.IsLoggedIn)
            .ThenByDescending(x => x.Rank.Level)
            .ThenBy(x => x.Name)
            .ThenBy(x => x.IsVacBanned)
            .ToList();

        private async Task<List<Account>> GetAccounts()
        {
            var steamAccounts = await _getAccountsUseCase.Execute();
            var accounts = (await Task.WhenAll(steamAccounts.ConvertAll(x => _accountMapper.FromSteamAccount(x))))
                .ToList();
            return SortAccounts(accounts);
        }

        private async void LoadAccounts()
        {
            LoadVisibilityConfig();

            if (IsLoading)
                return;

            IsLoading = true;
            var accounts = await GetAccounts();
            var whitelistedAccounts = accounts.Where(x => !x.IsBlacklisted).ToList();
            AllAccounts = accounts;
            WhitelistedAccounts = whitelistedAccounts;
            await Task.Run(DisplayAccountsForCurrentState);
            IsLoading = false;
        }

        private async void SendNotification(Account account)
        {
            if ((await _notificationConfigStorage.Get())?.IsAllowedToSendNotification != true)
                return;

            _notificationService.Send
            (
                new Notification
                (
                    title: account.Name,
                    message: account.Username,
                    account.ProfilePictureUrl
                )
            );
        }

        public async void OnAccountSelected(Account selectedAccount)
        {
            await _switchAccountUseCase.Execute(selectedAccount.Name);
            SendNotification(selectedAccount);
        }

        private void ShowInfo()
        {
            _infoService.ShowRepository();
        }
    }
}