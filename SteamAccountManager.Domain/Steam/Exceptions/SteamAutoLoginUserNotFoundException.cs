
namespace SteamAccountManager.Domain.Steam.Exceptions
{
    public class SteamAutoLoginUserNotFoundException : System.Exception
    {
        public SteamAutoLoginUserNotFoundException()
        {
        }

        public SteamAutoLoginUserNotFoundException(string message) : base(message)
        {
        }

        public SteamAutoLoginUserNotFoundException(string message, System.Exception inner) : base(message, inner)
        {
        }
    }
}
