using System;

namespace SteamAccountManager.Domain.Steam.Local.POCO
{
    public class SteamLoginUser
    {
        public string SteamId { get; private set; }
        public string AccountName { get; private set; }
        public bool IsLoginTokenValid { get; private set; }

        private SteamLoginUser(string steamId, string accountName, bool isLoginTokenValid)
        {
            SteamId = steamId;
            AccountName = accountName;
            IsLoginTokenValid = isLoginTokenValid;
        }

        public class Builder
        {
            private string _steamId = string.Empty;
            private string _accountName = string.Empty;
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
                return new SteamLoginUser(
                    steamId: _steamId, 
                    accountName: _accountName, 
                    isLoginTokenValid: _isLoginTokenValid
                    );
            }
        }
    }
}
