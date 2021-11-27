namespace SteamAccountManager.Infrastructure.Steam.Local.Dto
{
    public class LoginUserDto
    {
        public string SteamId { get; set; }
        public string AccountName { get; set; }
        public string PersonaName { get; set; }
        public bool PasswordRemembered { get; set; }
        public bool MostRecent { get; set; }
        public string Timestamp { get; set; }
    }
}
