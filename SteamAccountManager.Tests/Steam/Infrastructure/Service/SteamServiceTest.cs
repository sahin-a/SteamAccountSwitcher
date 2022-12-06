using Moq;
using SteamAccountManager.Application.Steam.Local.Logger;
using SteamAccountManager.Application.Steam.Local.Repository;
using SteamAccountManager.Application.Steam.Model;
using SteamAccountManager.Application.Steam.Service;
using SteamAccountManager.Domain.Steam.Exception;
using SteamAccountManager.Infrastructure.Steam.Service;
using SteamAccountManager.Tests.Steam.Infrastructure.Local.TestData;
using System.Collections.Generic;
using Xunit;

namespace SteamAccountManager.Tests.Steam.Infrastructure.Service
{
    public class SteamServiceTest
    {
        private readonly Mock<ISteamRepository> _steamRepositoryMock;
        private readonly Mock<ISteamProcessService> _steamProcessServiceMock;
        private readonly Mock<ISteamProfileService> _steamProfileServiceMock;
        private readonly Mock<ILogger> _loggerMock;
        private readonly ISteamService _sut;

        public SteamServiceTest()
        {
            _steamRepositoryMock = new Mock<ISteamRepository>(behavior: MockBehavior.Strict);
            _steamProcessServiceMock = new Mock<ISteamProcessService>(behavior: MockBehavior.Strict);
            _steamProfileServiceMock = new Mock<ISteamProfileService>(behavior: MockBehavior.Strict);
            _loggerMock = new Mock<ILogger>(behavior: MockBehavior.Loose);
            _sut = new SteamService(
                _steamRepositoryMock.Object,
                _steamProcessServiceMock.Object,
                _steamProfileServiceMock.Object,
                _loggerMock.Object
            );
        }

        [Fact]
        public void SwitchAccount_catches_UpdateAutoLoginUserFailedException()
        {
            var samFisherLoginUser = SteamUserTestData.GetSamFisherLoginUser();

            _steamRepositoryMock.Setup(repo => repo.UpdateAutoLoginUser(samFisherLoginUser.AccountName))
                .Throws<UpdateAutoLoginUserFailedException>();

            _sut.SwitchAccount(samFisherLoginUser.AccountName);
        }

        [Fact]
        public void SwitchAccount_returns_false_UpdateAutoLoginUserFailedException_is_thrown()
        {
            var samFisherLoginUser = SteamUserTestData.GetSamFisherLoginUser();

            _steamRepositoryMock.Setup(repo => repo.UpdateAutoLoginUser(samFisherLoginUser.AccountName))
                .Throws<UpdateAutoLoginUserFailedException>();

            Assert.False(_sut.SwitchAccount(samFisherLoginUser.AccountName));
        }

        [Fact]
        public async void GetAccounts_returns_Steam_Accounts()
        {
            var samFisherLoginUser = SteamUserTestData.GetSamFisherLoginUser();
            var samFisherProfile = SteamUserTestData.GetSamFisherProfile();

            var rainerLoginUser = SteamUserTestData.GetRainerLoginUser();
            var rainerProfile = SteamUserTestData.GetRainerProfile();

            _steamRepositoryMock.Setup(repo => repo.GetSteamLoginHistoryUsers())
                .ReturnsAsync(
                    new List<SteamLoginUser>
                    {
                        samFisherLoginUser,
                        rainerLoginUser
                    }
                ).Verifiable();

            _steamProfileServiceMock.Setup(service =>
                    service.GetProfileDetails(
                        samFisherLoginUser.SteamId,
                        rainerLoginUser.SteamId
                    )
                ).ReturnsAsync(
                    new List<SteamProfile>
                    {
                        samFisherProfile,
                        rainerProfile
                    }
                )
                .Verifiable();

            var result = await _sut.GetAccounts();

            var samFisherAccount = new SteamAccount.Builder()
                .SetData(samFisherProfile)
                .SetData(samFisherLoginUser)
                .Build();

            var rainerAccount = new SteamAccount.Builder()
                .SetData(rainerProfile)
                .SetData(rainerLoginUser)
                .Build();

            AssertSteamAccount(expected: samFisherAccount, actual: result[0]);
            AssertSteamAccount(expected: rainerAccount, actual: result[1]);
        }

        private void AssertSteamAccount(SteamAccount expected, SteamAccount actual)
        {
            Assert.Equal(expected.SteamId, actual.SteamId);
            Assert.Equal(expected.AccountName, actual.AccountName);
            Assert.Equal(expected.Username, actual.Username);
            Assert.Equal(expected.AvatarUrl, actual.AvatarUrl);
            Assert.Equal(expected.ProfileUrl, actual.ProfileUrl);
            Assert.Equal(expected.IsLoginValid, actual.IsLoginValid);
        }
    }
}