using SteamAccountManager.Application.Steam.Model;

namespace SteamAccountManager.Application.Steam.Service
{
    public interface ISteamProfileService
    {
        public Task<List<SteamProfile>> GetProfileDetails(params string[] steamIds);
    }
}