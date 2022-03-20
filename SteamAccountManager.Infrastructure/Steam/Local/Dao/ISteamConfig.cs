namespace SteamAccountManager.Infrastructure.Steam.Local.Dao
{
    public interface ISteamConfig
    {
        string GetSteamExecutablePath();
        string GetSteamPath();
        void SetAutoLoginUser(string accountName);
        public string GetAutoLoginUser();
        public string GetLoginUsersVdfPath();
    }
}