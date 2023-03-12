using System.Windows.Input;
using ReactiveUI;
using SteamAccountManager.Infrastructure.Steam.Local.Storage;

namespace SteamAccountManager.AvaloniaUI.ViewModels
{
    public class SettingsViewModel : RoutableViewModel
    {
        private readonly SteamApiKeyStorage _steamApiKeyStorage;

        public ICommand SaveApiKeyCommand { get; }
        public string WebApiKey { get; set; }

        public SettingsViewModel(IScreen screen, SteamApiKeyStorage apiKeyStorage) : base(screen)
        {
            _steamApiKeyStorage = apiKeyStorage;
            SaveApiKeyCommand = ReactiveCommand.Create((string key) => SaveApiKey(key));

            PrefillFields();
        }

        private void PrefillFields()
        {
            WebApiKey = _steamApiKeyStorage.Get();
        }

        private void SaveApiKey(string key)
        {
            _steamApiKeyStorage.Set(key);
        }
    }
}