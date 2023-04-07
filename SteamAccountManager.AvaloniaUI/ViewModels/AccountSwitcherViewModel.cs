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
using SteamAccountManager.Domain.Steam.Configuration.Model;
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
        private readonly EventBus _eventBus;
        private readonly InfoService _infoService;
        private readonly INotificationConfigStorage _notificationConfigStorage;

        public AdvancedObservableCollection<Account> Accounts { get; private set; }
        public ICommand ProfileClickedCommand { get; }
        public ICommand RefreshAccountsCommand { get; }
        public ICommand ShowInfoCommand { get; }
        public ICommand AddAccountCommand { get; }
        public Account? SelectedAccount { get; set; }
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
            _eventBus = eventBus;
            _infoService = infoService;

            Accounts = new AdvancedObservableCollection<Account>();
            ProfileClickedCommand = new ProfileClickedCommand();
            RefreshAccountsCommand = new QuickCommand(LoadAccounts);
            ShowInfoCommand = new QuickCommand(ShowInfo);
            AddAccountCommand = new QuickCommand(AddAccount);

            RegisterSubscriptions();
            LoadVisibilityConfig();
            LoadAccounts();
        }

        private void RegisterSubscriptions()
        {
            _eventBus.Subscribe(subscriberKey: GetType().Name, Events.ACCOUNTS_UPDATED,
                _ => LoadAccounts()
            );
            _eventBus.Subscribe(subscriberKey: GetType().Name, Events.PRIVACY_CONFIG_UPDATED,
                _ => LoadVisibilityConfig()
            );
        }

        private void LoadVisibilityConfig()
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

        private async void LoadAccounts()
        {
            LoadVisibilityConfig();

            if (IsLoading)
                return;

            IsLoading = true;
            await Task.Run(async () => Accounts.SetItems((await GetAccounts()).ToList()));
            IsLoading = false;
        }

        private void SendNotification(Account account)
        {
            if (_notificationConfigStorage.Get()?.IsAllowedToSendNotification != true)
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
            SelectedAccount = selectedAccount;
            SendNotification(selectedAccount);
        }

        private void ShowInfo()
        {
            _infoService.ShowRepository();
        }
    }
}