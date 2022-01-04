namespace SteamAccountManager.Domain.Steam.Exception
{
    public class UpdateAutoLoginUserFailedException : System.Exception
    {
        public UpdateAutoLoginUserFailedException() { }
        public UpdateAutoLoginUserFailedException(string message) : base(message) { }
    }
}
