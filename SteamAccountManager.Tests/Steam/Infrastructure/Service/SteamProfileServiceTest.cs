using Moq;
using SteamAccountManager.Application.Steam.Local.Logger;
using SteamAccountManager.Application.Steam.Model;
using SteamAccountManager.Application.Steam.Service;
using SteamAccountManager.Infrastructure.Steam.Exceptions;
using SteamAccountManager.Infrastructure.Steam.Remote.Dao;
using SteamAccountManager.Infrastructure.Steam.Remote.Dto;
using SteamAccountManager.Infrastructure.Steam.Service;
using System;
using System.Collections.Generic;
using Xunit;

namespace SteamAccountManager.Tests.Steam.Infrastructure.Service
{
    public class SteamProfileServiceTest
    {
        private readonly Mock<ISteamUserProvider> _steamPlayerSummaryProviderMock;
        private readonly Mock<ISteamPlayerService> _steamPlayerServiceMock;
        private readonly Mock<ILogger> _loggerMock;
        private readonly ISteamProfileService _sut;

        public SteamProfileServiceTest()
        {
            _steamPlayerSummaryProviderMock = new Mock<ISteamUserProvider>(behavior: MockBehavior.Strict);
            _steamPlayerServiceMock = new Mock<ISteamPlayerService>(behavior: MockBehavior.Strict);
            _loggerMock = new Mock<ILogger>(behavior: MockBehavior.Loose);
            _sut = new SteamProfileService(_steamPlayerSummaryProviderMock.Object, _steamPlayerServiceMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async void GetProfileDetails_returns_SteamProfiles()
        {
            var peterId = "44156412312345";
            var peterAvatar = "https://steamcommuntiy.com/id/peter/peter.jpg";
            var peterUrl = "https://steamcommuntiy.com/id/peter";
            var peterUsername = "Peter";
            var peterLevel = 1;

            var maffeiId = "53459345254242";
            var maffeiAvatar = "https://steamcommuntiy.com/id/maffei/maffei.jpg";
            var maffeiUrl = "https://steamcommuntiy.com/id/maffei";
            var maffeiUsername = "Maffei";
            var maffeiLevel = 2;

            _steamPlayerSummaryProviderMock.Setup(provider =>
                provider.GetSummariesAsync(peterId, maffeiId)
            ).ReturnsAsync(
                new List<PlayerSummary>
                {
                    new()
                    {
                        SteamId = peterId,
                        Avatar = new Uri(peterAvatar),
                        ProfileUrl = new Uri(peterUrl),
                        PersonaName = peterUsername
                    },
                    new()
                    {
                        SteamId = maffeiId,
                        Avatar = new Uri(maffeiAvatar),
                        ProfileUrl = new Uri(maffeiUrl),
                        PersonaName = maffeiUsername
                    }
                }
            ).Verifiable();

            _steamPlayerSummaryProviderMock.Setup(provider =>
                provider.GetPlayerBansAsync(peterId, maffeiId)
            ).ReturnsAsync(
                new List<PlayerBans>
                {
                    new() {SteamId = peterId, CommunityBanned = false, VacBanned = false},
                    new() {SteamId = maffeiId, CommunityBanned = false, VacBanned = false}
                }
            ).Verifiable();

            _steamPlayerServiceMock.Setup(provider => provider.GetPlayerLevelAsync(peterId))
                .ReturnsAsync(new SteamLevel { Level = peterLevel })
                .Verifiable();

            _steamPlayerServiceMock.Setup(provider => provider.GetPlayerLevelAsync(maffeiId))
                .ReturnsAsync(new SteamLevel { Level = maffeiLevel })
                .Verifiable();


            List<SteamProfile> profiles = await _sut.GetProfileDetails(peterId, maffeiId);

            _steamPlayerSummaryProviderMock.Verify(
                provider => provider.GetSummariesAsync(peterId, maffeiId),
                Times.Once
            );

            _steamPlayerSummaryProviderMock.Verify(provider =>
                    provider.GetPlayerBansAsync(peterId, maffeiId),
                Times.Once
            );

            _steamPlayerServiceMock.Verify(
                provider => provider.GetPlayerLevelAsync(peterId),
                Times.Once
            );

            _steamPlayerServiceMock.Verify(
                provider => provider.GetPlayerLevelAsync(maffeiId),
                Times.Once
            );

            var peterProfile = profiles.Find(profile => profile.Id == peterId);
            var maffeiProfile = profiles.Find(profile => profile.Id == maffeiId);

            Assert.Equal(expected: peterId, actual: peterProfile.Id);
            Assert.Equal(expected: peterAvatar, actual: peterProfile.Avatar);
            Assert.Equal(expected: peterUrl, actual: peterProfile.Url);
            Assert.Equal(expected: peterUsername, actual: peterProfile.Username);
            Assert.Equal(expected: peterLevel, actual: peterProfile.Level);

            Assert.Equal(expected: maffeiId, actual: maffeiProfile.Id);
            Assert.Equal(expected: maffeiAvatar, actual: maffeiProfile.Avatar);
            Assert.Equal(expected: maffeiUrl, actual: maffeiProfile.Url);
            Assert.Equal(expected: maffeiUsername, actual: maffeiProfile.Username);
            Assert.Equal(expected: maffeiLevel, actual: maffeiProfile.Level);
        }

        [Fact]
        public async void WHEN_GetPlayerBans_empty_GetProfileDetails_returns_SteamProfiles_bans_set_false()
        {
            var peterId = "44156412312345";
            var peterAvatar = "https://steamcommuntiy.com/id/peter/peter.jpg";
            var peterUrl = "https://steamcommuntiy.com/id/peter";
            var peterUsername = "Peter";
            var peterLevel = 1;

            _steamPlayerSummaryProviderMock.Setup(provider =>
                provider.GetSummariesAsync(peterId)
            ).ReturnsAsync(
                new List<PlayerSummary>
                {
                    new()
                    {
                        SteamId = peterId,
                        Avatar = new Uri(peterAvatar),
                        ProfileUrl = new Uri(peterUrl),
                        PersonaName = peterUsername
                    }
                }
            ).Verifiable();

            _steamPlayerSummaryProviderMock.Setup(provider => provider.GetPlayerBansAsync(peterId))
                .ReturnsAsync(new List<PlayerBans>())
                .Verifiable();

            _steamPlayerServiceMock.Setup(provider => provider.GetPlayerLevelAsync(It.IsAny<string>()))
                .ReturnsAsync(new SteamLevel { Level = peterLevel });

            List<SteamProfile> profiles = await _sut.GetProfileDetails(peterId);

            _steamPlayerSummaryProviderMock.Verify(
                provider => provider.GetSummariesAsync(peterId),
                Times.Once
            );

            _steamPlayerSummaryProviderMock.Verify(provider =>
                    provider.GetPlayerBansAsync(peterId),
                Times.Once
            );

            var peterProfile = profiles[0];

            Assert.False(peterProfile.IsVacBanned);
            Assert.False(peterProfile.IsCommunityBanned);
        }

        [Fact]
        public async void GetProfileDetails_catches_FailedToRetrieveSteamProfileException()
        {
            _steamPlayerSummaryProviderMock.Setup(provider => provider.GetSummariesAsync("1"))
                .ThrowsAsync(new FailedToRetrieveSteamProfileException());

            await _sut.GetProfileDetails("1");
        }

        [Fact]
        public async void GetProfileDetails_catches_InvalidSteamPlayerSummaryRequestException()
        {
            _steamPlayerSummaryProviderMock.Setup(provider => provider.GetSummariesAsync("1"))
                .ThrowsAsync(new IllegalSteamIdsCountException());

            await _sut.GetProfileDetails("1");
        }
    }
}