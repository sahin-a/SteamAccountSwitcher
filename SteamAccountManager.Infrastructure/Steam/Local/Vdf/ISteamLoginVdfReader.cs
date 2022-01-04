using System.Threading.Tasks;

namespace SteamAccountManager.Infrastructure.Steam.Local.Vdf
{
    public interface ISteamLoginVdfReader
    {
        public Task<string> GetLoginUsersVdfContent();
    }
}