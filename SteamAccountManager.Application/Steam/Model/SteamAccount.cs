namespace SteamAccountManager.Application.Steam.Model
{
    public class SteamAccount
    {
        public string SteamId { get; set; } = string.Empty;
        public string AccountName { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string AvatarUrl { get; set; } = string.Empty;
        public string ProfileUrl { get; set; } = string.Empty;
        public bool IsLoginValid { get; set; }
        public bool IsVacBanned { get; set; }
        public bool IsCommunityBanned { get; set; }
        public DateTime LastLogin { get; set; }
        public int Level { get; set; }

        private SteamAccount()
        {

        }

        public class Builder
        {
            private SteamAccount _steamAccount { get; set; }

            public Builder()
            {
                _steamAccount = new();
            }

            public Builder SetData(SteamProfile steamProfile)
            {
                _steamAccount.ProfileUrl = steamProfile.Url;
                _steamAccount.AvatarUrl = steamProfile.Avatar;
                _steamAccount.Username = steamProfile.Username;
                _steamAccount.IsVacBanned = steamProfile.IsVacBanned;
                _steamAccount.IsCommunityBanned = steamProfile.IsCommunityBanned;
                _steamAccount.Level = steamProfile.Level;

                return this;
            }

            public Builder SetData(SteamLoginUser steamLoginUser)
            {
                _steamAccount.SteamId = steamLoginUser.SteamId;
                _steamAccount.AccountName = steamLoginUser.AccountName;
                _steamAccount.IsLoginValid = steamLoginUser.IsLoginTokenValid;
                _steamAccount.LastLogin = steamLoginUser.LastLogin;

                return this;
            }

            public SteamAccount Build()
            {
                return _steamAccount;
            }
        }
    }
}