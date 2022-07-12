namespace SteamAccountManager.Infrastructure.Steam.Local.Storage
{
    public class UserAvatarStorage : LocalKeyValueStorage<string>
    {
        public UserAvatarStorage() : base("user_avatar_map")
        {

        }
    }
}
