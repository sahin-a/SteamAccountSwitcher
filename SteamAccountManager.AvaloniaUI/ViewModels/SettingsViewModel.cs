using System;
using System.Linq;
using System.Windows.Input;
using DynamicData;
using ReactiveUI;
using SteamAccountManager.AvaloniaUI.Common;
using SteamAccountManager.Domain.Steam.Configuration.Model;
using SteamAccountManager.Domain.Steam.Storage;

namespace SteamAccountManager.AvaloniaUI.ViewModels
{
    public class AccountDetailToggle
    {
        public string Title { get; set; }
        public ICommand ToggledCommand { get; set; }
        public bool IsToggled { get; set; }

        public AccountDetailType DetailType { get; set; }

        public AccountDetailToggle(AccountDetailType detailType, string title, ICommand toggledCommand, bool isToggled)
        {
            Title = title;
            ToggledCommand = toggledCommand;
            IsToggled = isToggled;
            DetailType = detailType;
        }
    }

    public class SettingsViewModel : RoutableViewModel
    {
        private readonly ISteamApiKeyStorage _steamApiKeyStorage;
        private readonly IPrivacyConfigStorage _privacyConfigStorage;


        public AdvancedObservableCollection<AccountDetailToggle> AccountDetailsToggles { get; } = new();

        public ICommand SaveApiKeyCommand { get; }
        public string WebApiKey { get; set; }

        public SettingsViewModel
        (
            IScreen screen,
            ISteamApiKeyStorage apiKeyStorage,
            IPrivacyConfigStorage privacyConfigStorage
        ) : base(screen)
        {
            _steamApiKeyStorage = apiKeyStorage;
            _privacyConfigStorage = privacyConfigStorage;
            SaveApiKeyCommand = ReactiveCommand.Create((string key) => SaveApiKey(key));

            InitializeControls();
            PrefillFields();
        }

        private void InitializeControls()
        {
            CreateAccountDetailToggles();
        }

        private void PrefillFields()
        {
            WebApiKey = _steamApiKeyStorage.Get();

            var privacyConfig = _privacyConfigStorage.Get()?.DetailSettings;
            if (privacyConfig is not null)
            {
                foreach (var toggle in AccountDetailsToggles)
                {
                    toggle.IsToggled = privacyConfig
                        .FirstOrDefault(x => x.DetailType == toggle.DetailType)?.IsEnabled ?? false;
                }
            }
        }

        private void CreateAccountDetailToggles()
        {
            var detailToggledCommand =
                ReactiveCommand.Create((AccountDetailToggle detailToggle) => DetailToggled(detailToggle));

            var toggles = Enum.GetValues<AccountDetailType>().Select(x =>
                new AccountDetailToggle(x, title: x.ToTitle(), detailToggledCommand, isToggled: true));

            AccountDetailsToggles.SetItems(toggles.ToList());
        }

        private void DetailToggled(AccountDetailToggle detailToggle)
        {
            var privacyConfig = _privacyConfigStorage.Get();
            if (privacyConfig is not null)
            {
                var setting = privacyConfig.DetailSettings.FirstOrDefault(x => x.DetailType == detailToggle.DetailType);
                privacyConfig.DetailSettings.ReplaceOrAdd(original: setting,
                    replaceWith: new(detailToggle.DetailType, detailToggle.IsToggled));

                _privacyConfigStorage.Set(privacyConfig);
            }
            else
            {
                var settings = AccountDetailsToggles.Select(x => new DetailSetting(x.DetailType, x.IsToggled));
                _privacyConfigStorage.Set(new PrivacyConfig(settings.ToList()));
            }
        }

        private void SaveApiKey(string key)
        {
            _steamApiKeyStorage.Set(key);
        }
    }

    static class AccountDetailTypeExtensions
    {
        // TODO: refactor this when localization story is being done
        public static string ToTitle(this AccountDetailType type)
        {
            switch (type)
            {
                case AccountDetailType.LoginName:
                    return "Login Name";
                case AccountDetailType.Level:
                    return "Level";
                case AccountDetailType.Avatar:
                    return "Avatar";
                case AccountDetailType.BanStatus:
                    return "Ban Status";
            }

            return "";
        }
    }
}