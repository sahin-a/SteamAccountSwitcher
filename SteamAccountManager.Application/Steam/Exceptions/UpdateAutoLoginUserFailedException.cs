namespace SteamAccountManager.Application.Steam.Exceptions
{
    public class UpdateAutoLoginUserFailedException : System.Exception
    {
        public UpdateAutoLoginUserFailedException() { }
        public UpdateAutoLoginUserFailedException(string message) : base(message) { }
    }
}
