namespace SteamAccountManager.Application.Steam.Model
{
    public class LoginUser
    {
        public string SteamId { get; private set; } = string.Empty;
        public string AccountName { get; private set; } = string.Empty;
        public string Username { get; set; }
        public bool IsLoginTokenValid { get; private set; }
        public DateTime LastLogin { get; set; }

        private LoginUser()
        {
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            LoginUser other = (LoginUser)obj;

            return SteamId == other.SteamId
                && AccountName == other.AccountName
                && Username == other.Username
                && IsLoginTokenValid == other.IsLoginTokenValid
                && LastLogin == other.LastLogin;
        }

        public class Builder
        {
            private LoginUser _steamLoginUser;

            public Builder()
            {
                _steamLoginUser = new();
            }

            public Builder SetSteamId(string steamId)
            {
                _steamLoginUser.SteamId = steamId;
                return this;
            }

            public Builder SetAccountName(string accountName)
            {
                _steamLoginUser.AccountName = accountName;
                return this;
            }

            public Builder SetUsername(string username)
            {
                _steamLoginUser.Username = username;
                return this;
            }

            public Builder SetIsLoginTokenValid(bool isLoginTokenValid)
            {
                _steamLoginUser.IsLoginTokenValid = isLoginTokenValid;
                return this;
            }

            public Builder SetLastLogin(DateTime lastLogin)
            {
                _steamLoginUser.LastLogin = lastLogin;
                return this;
            }

            public LoginUser Build()
            {
                return _steamLoginUser;
            }
        }
    }
}