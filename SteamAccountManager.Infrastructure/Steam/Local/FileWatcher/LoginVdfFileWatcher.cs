using System.IO;
using SteamAccountManager.Domain.Common.EventSystem;
using SteamAccountManager.Domain.Steam.Observables;
using SteamAccountManager.Infrastructure.Steam.Local.Dao;

namespace SteamAccountManager.Infrastructure.Steam.Local.FileWatcher
{
    internal class LoginVdfFileWatcher : IAccountStorageWatcher
    {
        private FileSystemWatcher? watcher;
        private readonly EventBus _eventBus;
        private readonly ISteamConfig _steamConfig;

        public LoginVdfFileWatcher(ISteamConfig steamConfig, EventBus eventBus)
        {
            _steamConfig = steamConfig;
            _eventBus = eventBus;
        }

        public void Start()
        {
            watcher = new FileSystemWatcher(Path.GetDirectoryName(_steamConfig.GetLoginUsersVdfPath()));
            watcher.NotifyFilter = NotifyFilters.Attributes;

            watcher.Changed += OnChanged;
            watcher.Filter = Path.GetFileName(_steamConfig.GetLoginUsersVdfPath());
            watcher.IncludeSubdirectories = true;
            watcher.EnableRaisingEvents = true;
        }

        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            _eventBus.Notify(Events.ACCOUNTS_UPDATED, null);
        }
    }
}