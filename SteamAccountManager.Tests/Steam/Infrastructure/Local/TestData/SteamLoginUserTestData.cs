using SteamAccountManager.Domain.Steam.Local.POCO;

namespace SteamAccountManager.Tests.Steam.Infrastructure.Local.TestData
{
    public class SteamLoginUserTestData
    {
        private static SteamLoginUser createSteamLoginUser(
            string steamId,
            string accountName,
            bool isLoginTokenValid
        ) => new SteamLoginUser.Builder()
            .SetSteamId(steamId)
            .SetAccountName(accountName)
            .SetIsLoginTokenValid(isLoginTokenValid)
            .Build();

        public static SteamLoginUser GetSamFisher() => createSteamLoginUser(
            steamId: "415341232123",
            accountName: "SamFisher",
            isLoginTokenValid: true
        );

        public static SteamLoginUser GetRainer() => createSteamLoginUser(
            steamId: "4509234026243",
            accountName: "RainerW",
            isLoginTokenValid: false
        );
    }
}