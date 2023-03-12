namespace SteamAccountManager.Domain.Steam.Service
{
    public interface ISteamProcessService
    {
        public bool KillSteam();
        public Task StartSteam();
    }
}
