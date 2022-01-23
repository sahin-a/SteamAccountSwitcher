using SteamAccountManager.Application.Steam.Model;

namespace SteamAccountManager.Application.Steam.Service
{
    public interface ISteamProfileService
    {
        public Task<List<Profile>> GetProfileDetails(params string[] steamIds);
    }
}