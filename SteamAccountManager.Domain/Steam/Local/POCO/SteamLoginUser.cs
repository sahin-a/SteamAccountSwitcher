namespace SteamAccountManager.Domain.Steam.Local.POCO
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
            private string _steamId;
            private string _accountName;
            private bool _isLoginTokenValid;

            public Builder SetSteamId(string steamId)
            {
                _steamId = steamId;
                return this;
            }

            public Builder SetAccountName(string accountName)
            {
                _accountName = accountName;
                return this;
            }

            public Builder SetIsLoginTokenValid(bool isLoginTokenValid)
            {
                _isLoginTokenValid = isLoginTokenValid;
                return this;
            }

            public SteamLoginUser Build()
            {
                return new SteamLoginUser()
                {
                    AccountName = _accountName,
                    SteamId = _steamId,
                    IsLoginTokenValid = _isLoginTokenValid
                };
            }
        }
    }
}