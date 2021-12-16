using SteamAccountManager.Application.Steam.Model;

namespace SteamAccountManager.Tests.Steam.Infrastructure.Local.TestData
{
    public class SteamUserTestData
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

        public static SteamProfile GetSamFisherProfile() => new SteamProfile
        {
            Id = "415341232123",
            Avatar = "samfisher.jpg",
            Username = "Sam Fisher oof",
            Url = "https://steamcommunity.com/id/samfisher"
        };

        public static SteamLoginUser GetSamFisherLoginUser() => createSteamLoginUser(
            steamId: "415341232123",
            accountName: "SamFisher",
            isLoginTokenValid: true
        );
        
        public static SteamProfile GetRainerProfile() => new SteamProfile
        {
            Id = "4509234026243",
            Avatar = "rainer.jpg",
            Username = "Rainer Winkler",
            Url = "https://steamcommunity.com/id/rainerwinkler"
        };

        public static SteamLoginUser GetRainerLoginUser() => createSteamLoginUser(
            steamId: "4509234026243",
            accountName: "RainerW",
            isLoginTokenValid: false
        );
    }
}