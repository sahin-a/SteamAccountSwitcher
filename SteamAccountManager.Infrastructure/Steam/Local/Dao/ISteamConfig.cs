namespace SteamAccountManager.Infrastructure.Steam.Local.Dao
{
    public interface ISteamConfig
    {
        string GetSteamExecutablePath();
        string GetSteamPath();
        
        bool SetAutoLoginUser(string accountName);
    }
}