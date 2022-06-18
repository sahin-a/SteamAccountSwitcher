using SteamAccountManager.Application.Steam.Model;

namespace SteamAccountManager.Tests.Steam.Infrastructure.Local.TestData
{
    public class SteamUserTestData
    {
        private static LoginUser createSteamLoginUser(
            string steamId,
            string accountName,
            string username,
            bool isLoginTokenValid
        ) => new LoginUser.Builder()
            .SetSteamId(steamId)
            .SetAccountName(accountName)
            .SetUsername(username)
            .SetIsLoginTokenValid(isLoginTokenValid)
            .Build();

        public static Profile GetSamFisherProfile() => new Profile
        {
            Id = "415341232123",
            AvatarUrl = "samfisher.jpg",
            Username = "Sam Fisher oof",
            Url = "https://steamcommunity.com/id/samfisher",
            IsVacBanned = false,
            IsCommunityBanned = false
        };

        public static LoginUser GetSamFisherLoginUser() => createSteamLoginUser(
            steamId: "415341232123",
            accountName: "SamFisher",
            username: "Sam",
            isLoginTokenValid: true
        );

        public static Profile GetRainerProfile() => new Profile
        {
            Id = "4509234026243",
            AvatarUrl = "rainer.jpg",
            Username = "Rainer Winkler",
            Url = "https://steamcommunity.com/id/rainerwinkler",
            IsVacBanned = true,
            IsCommunityBanned = true
        };

        public static LoginUser GetRainerLoginUser() => createSteamLoginUser(
            steamId: "4509234026243",
            accountName: "RainerW",
            username: "Übermensch",
            isLoginTokenValid: false
        );
    }
}