using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using SteamAccountManager.Application.Steam.Model;
using SteamAccountManager.Application.Steam.Service;
using SteamAccountManager.Infrastructure.Steam.Exceptions;
using SteamAccountManager.Infrastructure.Steam.Remote.Dao;
using SteamAccountManager.Infrastructure.Steam.Remote.Dto;
using SteamAccountManager.Infrastructure.Steam.Service;
using Xunit;

namespace SteamAccountManager.Tests.Steam.Infrastructure.Service
{
    public class SteamProfileServiceTest
    {
        private readonly Mock<ISteamPlayerSummaryProvider> _steamPlayerSummaryProviderMock;
        private readonly ISteamProfileService _sut;

        public SteamProfileServiceTest()
        {
            _steamPlayerSummaryProviderMock = new Mock<ISteamPlayerSummaryProvider>(behavior: MockBehavior.Strict);
            _sut = new SteamProfileService(_steamPlayerSummaryProviderMock.Object);
        }

        [Fact]
        public async void GetProfileDetails_returns_SteamProfiles()
        {
            var peterId = "44156412312345";
            var peterAvatar = "https://steamcommuntiy.com/id/peter/peter.jpg";
            var peterUrl = "https://steamcommuntiy.com/id/peter";
            var peterUsername = "Peter";

            var maffeiId = "53459345254242";
            var maffeiAvatar = "https://steamcommuntiy.com/id/maffei/maffei.jpg";
            var maffeiUrl = "https://steamcommuntiy.com/id/maffei";
            var maffeiUsername = "Maffei";

            _steamPlayerSummaryProviderMock.Setup(provider =>
                provider.GetSummaryAsync(peterId, maffeiId)
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

            List<SteamProfile> profiles = await _sut.GetProfileDetails(peterId, maffeiId);

            _steamPlayerSummaryProviderMock.Verify(
                provider => provider.GetSummaryAsync(peterId, maffeiId),
                Times.Once
            );

            var peterProfile = profiles.Find(profile => profile.Id == peterId);
            var maffeiProfile = profiles.Find(profile => profile.Id == maffeiId);

            Assert.Equal(expected: peterId, actual: peterProfile.Id);
            Assert.Equal(expected: peterAvatar, actual: peterProfile.Avatar);
            Assert.Equal(expected: peterUrl, actual: peterProfile.Url);
            Assert.Equal(expected: peterUsername, actual: peterProfile.Username);

            Assert.Equal(expected: maffeiId, actual: maffeiProfile.Id);
            Assert.Equal(expected: maffeiAvatar, actual: maffeiProfile.Avatar);
            Assert.Equal(expected: maffeiUrl, actual: maffeiProfile.Url);
            Assert.Equal(expected: maffeiUsername, actual: maffeiProfile.Username);
        }

        [Fact]
        public async void GetProfileDetails_catches_FailedToRetrieveSteamProfileException()
        {
            _steamPlayerSummaryProviderMock.Setup(provider => provider.GetSummaryAsync("1"))
                .ThrowsAsync(new FailedToRetrieveSteamProfileException());

            await _sut.GetProfileDetails("1");
        }
        
        [Fact]
        public async void GetProfileDetails_catches_InvalidSteamPlayerSummaryRequestException()
        {
            _steamPlayerSummaryProviderMock.Setup(provider => provider.GetSummaryAsync("1"))
                .ThrowsAsync(new InvalidSteamPlayerSummaryRequestException());

            await _sut.GetProfileDetails("1");
        }
    }
}