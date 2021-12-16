namespace SteamAccountManager.Application.Steam.Model
{
    public class SteamLoginUser
    {
        public string SteamId { get; private set; }
        public string AccountName { get; private set; }
        public bool IsLoginTokenValid { get; private set; }

        private SteamLoginUser()
        {
        }

        public class Builder
        {
            private SteamLoginUser _steamLoginUser;

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

            public Builder SetIsLoginTokenValid(bool isLoginTokenValid)
            {
                _steamLoginUser.IsLoginTokenValid = isLoginTokenValid;
                return this;
            }

            public SteamLoginUser Build()
            {
                return _steamLoginUser;
            }
        }
    }
}