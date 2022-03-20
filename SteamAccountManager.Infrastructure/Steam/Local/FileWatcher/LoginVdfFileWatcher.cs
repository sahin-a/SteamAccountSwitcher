using SteamAccountManager.Infrastructure.Steam.Local.Dao;
using SteamAccountManager.Infrastructure.Steam.Local.Observer;
using System.IO;

namespace SteamAccountManager.Infrastructure.Steam.Local.FileWatcher
{
    internal class LoginVdfFileWatcher : AccountStorageObserver
    {
        private readonly FileSystemWatcher watcher;

        public LoginVdfFileWatcher(ISteamConfig steamConfig)
        {
            watcher = new FileSystemWatcher(Path.GetDirectoryName(steamConfig.GetLoginUsersVdfPath()));
            watcher.NotifyFilter = NotifyFilters.Attributes
                                 | NotifyFilters.CreationTime
                                 | NotifyFilters.DirectoryName
                                 | NotifyFilters.FileName
                                 | NotifyFilters.LastAccess
                                 | NotifyFilters.LastWrite
                                 | NotifyFilters.Security
                                 | NotifyFilters.Size;

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
