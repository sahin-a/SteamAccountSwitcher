using Moq;
using Newtonsoft.Json;
using RestSharp;
using SteamAccountManager.Infrastructure.Steam.Remote.Dao;
using SteamAccountManager.Infrastructure.Steam.Remote.Dto;
using System;
using System.Linq;
using SteamAccountManager.Domain.Steam.Local.Logger;
using SteamAccountManager.Infrastructure.Steam.Exceptions;
using Xunit;

namespace SteamAccountManager.Tests.Steam.Infrastructure.Remote.Dao
{
    public class SteamUserProviderTest
    {
        private readonly Mock<ISteamWebClient> _steamWebClientMock;
        private readonly Mock<ILogger> _loggerMock;
        private readonly SteamUserProvider _sut;

        public SteamUserProviderTest()
        {
            _steamWebClientMock = new Mock<ISteamWebClient>(behavior: MockBehavior.Strict);
            _loggerMock = new Mock<ILogger>(behavior: MockBehavior.Loose);
            _sut = new SteamUserProvider(_steamWebClientMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async void GetSummary_returns_ProfileSummary_If_Successful()
        {
            var responseJson =
                "{\"response\":{\"players\":[{\"steamid\":\"76561197960435530\",\"communityvisibilitystate\":3,\"profilestate\":1,\"personaname\":\"Robin\",\"profileurl\":\"https://steamcommunity.com/id/robinwalker/\",\"avatar\":\"https://steamcdn-a.akamaihd.net/steamcommunity/public/images/avatars/f1/f1dd60a188883caf82d0cbfccfe6aba0af1732d4.jpg\",\"avatarmedium\":\"https://steamcdn-a.akamaihd.net/steamcommunity/public/images/avatars/f1/f1dd60a188883caf82d0cbfccfe6aba0af1732d4_medium.jpg\",\"avatarfull\":\"https://steamcdn-a.akamaihd.net/steamcommunity/public/images/avatars/f1/f1dd60a188883caf82d0cbfccfe6aba0af1732d4_full.jpg\",\"avatarhash\":\"f1dd60a188883caf82d0cbfccfe6aba0af1732d4\",\"personastate\":3,\"realname\":\"Robin Walker\",\"primaryclanid\":\"103582791429521412\",\"timecreated\":1063407589,\"personastateflags\":0,\"loccountrycode\":\"US\",\"locstatecode\":\"WA\",\"loccityid\":3961}]}}";
            var steamId = "76561197960435530";

            _steamWebClientMock.Setup(client => client.ExecuteAsync<SteamPlayerSummariesDto>(It.IsAny<RestRequest>()))
                .ReturnsAsync(JsonConvert.DeserializeObject<SteamPlayerSummariesDto>(responseJson))
                .Verifiable();

            var profileSummaries = await _sut.GetSummariesAsync(steamId);

            _steamWebClientMock.Verify(client =>
                    client.ExecuteAsync<SteamPlayerSummariesDto>(It.IsAny<RestRequest>()),
                Times.Once
            );

            var profileSummary = profileSummaries.First();

            Assert.Equal(expected: "76561197960435530", actual: profileSummary.SteamId);
            Assert.Equal(expected: "Robin", actual: profileSummary.PersonaName);
            Assert.Equal(expected: "https://steamcommunity.com/id/robinwalker/",
                actual: profileSummary.ProfileUrl.AbsoluteUri);
            Assert.Equal(
                expected:
                "https://steamcdn-a.akamaihd.net/steamcommunity/public/images/avatars/f1/f1dd60a188883caf82d0cbfccfe6aba0af1732d4_full.jpg",
                actual: profileSummary.Avatar.AbsoluteUri
            );
            Assert.Equal(expected: -1L, actual: profileSummary.LastLogOff);
            Assert.Equal(expected: 1063407589L, actual: profileSummary.TimeCreated);
        }

        [Fact]
        public async void GetSummary_throws_InvalidSteamIdsCountException_if_no_steam_ids_supplied()
        {
            await Assert.ThrowsAsync<IllegalSteamIdsCountException>(() => _sut.GetSummariesAsync());
        }

        [Fact]
        public async void GetSummary_throws_FailedToRetrieveSteamProfileException_when_Exception_thrown()
        {
            _steamWebClientMock.Setup(client =>
                client.ExecuteAsync<SteamPlayerSummariesDto>(It.IsAny<RestRequest>())
            ).ThrowsAsync(new Exception());

            await Assert.ThrowsAsync<FailedToRetrieveSteamProfileException>(() =>
                _sut.GetSummariesAsync("2")
            );
        }
    }
}