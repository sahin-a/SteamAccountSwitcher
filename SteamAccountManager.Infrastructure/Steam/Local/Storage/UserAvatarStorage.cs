using SteamAccountManager.Domain.Steam.Local.Logger;
using SteamAccountManager.Infrastructure.Steam.Local.Dao;

namespace SteamAccountManager.Infrastructure.Steam.Local.Storage
{
    public class UserAvatarStorage : LocalKeyValueStorage<string>
    {
        public UserAvatarStorage(ILogger logger, IFileProvider fileProvider, string name = "user_avatar_mapping") :
            base(logger, fileProvider, name)
        {
        }
    }
}