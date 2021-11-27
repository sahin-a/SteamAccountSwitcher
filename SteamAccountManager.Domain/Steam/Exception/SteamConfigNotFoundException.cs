namespace SteamAccountManager.Domain.Steam.Exception
{
    public class SteamConfigNotFoundException : System.Exception
    {
        public SteamConfigNotFoundException(string message = "Steam Config not found!") : base(message)
        {
        }
    }
}