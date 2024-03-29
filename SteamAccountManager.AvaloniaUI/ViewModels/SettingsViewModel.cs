﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using DynamicData;
using ReactiveUI;
using SteamAccountManager.AvaloniaUI.Common;
using SteamAccountManager.AvaloniaUI.Services;
using SteamAccountManager.Domain.Common.EventSystem;
using SteamAccountManager.Domain.Steam.Configuration.Model;
using SteamAccountManager.Domain.Steam.Storage;

namespace SteamAccountManager.AvaloniaUI.ViewModels
{
    public class SettingsViewModel : RoutableViewModel
    {
        private readonly ISteamApiKeyStorage _steamApiKeyStorage;
        private readonly IPrivacyConfigStorage _privacyConfigStorage;
        private readonly EventBus _eventBus;
        private readonly INotificationConfigStorage _notificationConfigStorage;
        private readonly IRichPresenceConfigStorage _richPresenceConfigStorage;
        private readonly InfoService _infoService;

        public AdvancedObservableCollection<AccountDetailToggle> AccountDetailsToggles { get; } = new();
        public AdvancedObservableCollection<SettingsToggle> SettingsToggles { get; } = new();
        public ICommand RichPresenceToggleCommand { get; }
        public ICommand ShowRetrieveApiKeyPageCommand { get; }
        public bool IsRichPresenceEnabled { get; set; }
        private string _webApiKey = "";

        public string WebApiKey
        {
            get => _webApiKey;
            set
            {
                _webApiKey = value;
                UpdateApiKey(value);
            }
        }

        public SettingsViewModel
        (
            IScreen screen,
            ISteamApiKeyStorage apiKeyStorage,
            IPrivacyConfigStorage privacyConfigStorage,
            IRichPresenceConfigStorage richPresenceConfigStorage,
            INotificationConfigStorage notificationConfigStorage,
            EventBus eventBus,
            InfoService infoService
        ) : base(screen)
        {
            _steamApiKeyStorage = apiKeyStorage;
            _privacyConfigStorage = privacyConfigStorage;
            _notificationConfigStorage = notificationConfigStorage;
            _richPresenceConfigStorage = richPresenceConfigStorage;
            _eventBus = eventBus;
            _infoService = infoService;

            RichPresenceToggleCommand = ReactiveCommand.Create(UpdateRichPresenceConfig);
            ShowRetrieveApiKeyPageCommand = ReactiveCommand.Create(ShowRetrieveApiKeyPage);

            InitializeControls();
            PrefillFields();
        }

        private void InitializeControls()
        {
            CreateAccountDetailToggles();
            CreateSettingsToggles();
        }

        private void UpdateRichPresenceConfig()
        {
            _richPresenceConfigStorage.Set(new RichPresenceConfig
            {
                IsEnabled = IsRichPresenceEnabled
            });
            _eventBus.Notify(Events.RICH_PRESENCE_CONFIG_UPDATED, null);
        }

        private void ShowRetrieveApiKeyPage()
        {
            _infoService.ShowRetrieveApiKey();
        }

        private async void CreateSettingsToggles()
        {
            SettingsToggles.SetItems(
                new List<SettingsToggle>
                {
                    new
                    (
                        title: "Allow Notifications",
                        isToggled: (await _notificationConfigStorage.Get())?.IsAllowedToSendNotification == true,
                        toggledCommand: ReactiveCommand.Create<SettingsToggle>(toggle =>
                            _notificationConfigStorage.Set(
                                new NotificationConfig(isAllowedToSendNotification: toggle.IsToggled)
                            )
                        )
                    )
                }
            );
        }

        private async void PrefillFields()
        {
            WebApiKey = await _steamApiKeyStorage.Get() ?? "";

            var privacyConfig = (await _privacyConfigStorage.Get())?.DetailSettings;
            if (privacyConfig is not null)
            {
                foreach (var toggle in AccountDetailsToggles)
                {
                    toggle.IsToggled = privacyConfig
                        .FirstOrDefault(x => x.DetailType == toggle.DetailType)?.IsEnabled ?? false;
                }
            }

            IsRichPresenceEnabled = (await _richPresenceConfigStorage.Get())?.IsEnabled ?? true;
        }

        private void CreateAccountDetailToggles()
        {
            var detailToggledCommand =
                ReactiveCommand.Create((SettingsToggle settingsToggle) => DetailToggled(settingsToggle));

            var toggles = Enum.GetValues<AccountDetailType>().Select(x =>
                new AccountDetailToggle(detailType: x, title: x.ToTitle(), isToggled: true, detailToggledCommand));

            AccountDetailsToggles.SetItems(toggles.ToList());
        }

        private async void DetailToggled(SettingsToggle detailToggle)
        {
            if (detailToggle is not AccountDetailToggle toggle)
                return;

            var privacyConfig = await _privacyConfigStorage.Get();
            if (privacyConfig is not null)
            {
                var setting =
                    privacyConfig.DetailSettings.FirstOrDefault(x => x.DetailType == toggle.DetailType);
                privacyConfig.DetailSettings.ReplaceOrAdd(original: setting,
                    replaceWith: new(toggle.DetailType, detailToggle.IsToggled));

                _privacyConfigStorage.Set(privacyConfig);
            }
            else
            {
                var settings = AccountDetailsToggles.Select(x => new DetailSetting(x.DetailType, x.IsToggled));
                _privacyConfigStorage.Set(new PrivacyConfig(settings.ToList()));
            }

            _eventBus.Notify(Events.PRIVACY_CONFIG_UPDATED, null);
        }

        private void UpdateApiKey(string key)
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

    public class SettingsToggle
    {
        public string Title { get; set; }
        public ICommand ToggledCommand { get; set; }
        public bool IsToggled { get; set; }

        public SettingsToggle(string title, bool isToggled, ICommand toggledCommand)
        {
            Title = title;
            ToggledCommand = toggledCommand;
            IsToggled = isToggled;
        }
    }

    public class AccountDetailToggle : SettingsToggle
    {
        public AccountDetailType DetailType { get; set; }

        public AccountDetailToggle
        (
            AccountDetailType detailType,
            string title,
            bool isToggled,
            ICommand toggledCommand
        ) : base(title, isToggled, toggledCommand)
        {
            Title = title;
            ToggledCommand = toggledCommand;
            IsToggled = isToggled;
            DetailType = detailType;
        }
    }
}