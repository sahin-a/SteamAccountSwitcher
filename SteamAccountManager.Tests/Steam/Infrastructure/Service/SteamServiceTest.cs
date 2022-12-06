using System.Collections.Generic;
using Moq;
using SteamAccountManager.Domain.Steam.Exception;
using SteamAccountManager.Domain.Steam.Local.Logger;
using SteamAccountManager.Domain.Steam.Local.POCO;
using SteamAccountManager.Domain.Steam.Local.Repository;
using SteamAccountManager.Domain.Steam.Service;
using SteamAccountManager.Infrastructure.Steam.Service;
using SteamAccountManager.Tests.Steam.Infrastructure.Local.TestData;
using Xunit;

namespace SteamAccountManager.Tests.Steam.Infrastructure.Service
{
    public class SteamServiceTest
    {
        private readonly Mock<ISteamRepository> _steamRepositoryMock;
        private readonly Mock<ISteamProcessService> _steamProcessServiceMock;
        private readonly Mock<ILogger> _loggerMock;
        private readonly ISteamService _sut;

        public SteamServiceTest()
        {
            _steamRepositoryMock = new Mock<ISteamRepository>(behavior: MockBehavior.Strict);
            _steamProcessServiceMock = new Mock<ISteamProcessService>(behavior: MockBehavior.Strict);
            _loggerMock = new Mock<ILogger>(behavior: MockBehavior.Loose);
            _sut = new SteamService(_steamRepositoryMock.Object, _steamProcessServiceMock.Object, _loggerMock.Object);
        }

        [Fact]
        public void SwitchAccount_catches_UpdateAutoLoginUserFailedException()
        {
            var samFisherAccount = SteamLoginUserTestData.GetSamFisher();

            _steamRepositoryMock.Setup(repo => repo.UpdateAutoLoginUser(samFisherAccount))
                .Throws<UpdateAutoLoginUserFailedException>();

            _sut.SwitchAccount(samFisherAccount);
        }

        [Fact]
        public void SwitchAccount_returns_false_UpdateAutoLoginUserFailedException_is_thrown()
        {
            var samFisherAccount = SteamLoginUserTestData.GetSamFisher();

            _steamRepositoryMock.Setup(repo => repo.UpdateAutoLoginUser(samFisherAccount))
                .Throws<UpdateAutoLoginUserFailedException>();

            Assert.False(_sut.SwitchAccount(samFisherAccount));
        }

        [Fact]
        public async void GetAccounts_returns_steam_users()
        {
            var samFisherAccount = SteamLoginUserTestData.GetSamFisher();
            var rainerAccount = SteamLoginUserTestData.GetRainer();
            
            _steamRepositoryMock.Setup(repo => repo.GetSteamLoginHistoryUsers())
                .ReturnsAsync(
                    new List<SteamLoginUser>
                    {
                        samFisherAccount,
                        rainerAccount
                    }
                ).Verifiable();

            var result = await _sut.GetAccounts();
            
            Assert.Equal(expected: samFisherAccount, actual: result[0]);
            Assert.Equal(expected: rainerAccount, actual: result[1]);
        }
    }
}