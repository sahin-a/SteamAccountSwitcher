namespace SteamAccountManager.Application.Steam.Exceptions
{
    public class SteamConfigNotFoundException : System.Exception
    {
        public SteamConfigNotFoundException(string message = "Steam Config not found!") : base(message)
        {
        }
    }
}