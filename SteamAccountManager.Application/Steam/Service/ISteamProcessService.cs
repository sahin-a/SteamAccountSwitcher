namespace SteamAccountManager.Application.Steam.Service
{
    public interface ISteamProcessService
    {
        public bool KillSteam();
        public void StartSteam();
    }
}
