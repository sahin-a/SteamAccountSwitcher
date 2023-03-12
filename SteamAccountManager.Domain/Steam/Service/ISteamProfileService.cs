using SteamAccountManager.Domain.Steam.Model;

namespace SteamAccountManager.Domain.Steam.Service
{
    public interface ISteamProfileService
    {
        public Task<List<Profile>> GetProfileDetails(params string[] steamIds);
    }
}