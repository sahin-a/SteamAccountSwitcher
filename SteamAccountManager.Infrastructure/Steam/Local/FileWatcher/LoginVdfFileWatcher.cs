using SteamAccountManager.Domain.Common.Observable;
using SteamAccountManager.Domain.Steam.Model;
using SteamAccountManager.Domain.Steam.Observables;
using SteamAccountManager.Infrastructure.Steam.Local.Dao;
using System.Collections.Generic;
using System.IO;

namespace SteamAccountManager.Infrastructure.Steam.Local.FileWatcher
{
    internal class LoginVdfFileWatcher : BaseObservable<List<Account>?>, IAccountStorageObservable
    {
        private readonly FileSystemWatcher watcher;

        public LoginVdfFileWatcher(ISteamConfig steamConfig)
        {
            watcher = new FileSystemWatcher(Path.GetDirectoryName(steamConfig.GetLoginUsersVdfPath()));
            watcher.NotifyFilter = NotifyFilters.Attributes;

            watcher.Changed += OnChanged;
            watcher.Filter = Path.GetFileName(steamConfig.GetLoginUsersVdfPath());
            watcher.IncludeSubdirectories = true;
            watcher.EnableRaisingEvents = true;
        }

        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            Notify(null);
        }
    }
}
