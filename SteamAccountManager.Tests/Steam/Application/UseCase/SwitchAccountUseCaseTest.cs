using Moq;
using SteamAccountManager.Domain.Steam.Exceptions;
using SteamAccountManager.Domain.Steam.Local.Logger;
using SteamAccountManager.Domain.Steam.Local.Repository;
using SteamAccountManager.Domain.Steam.Service;
using SteamAccountManager.Domain.Steam.UseCase;
using SteamAccountManager.Tests.Steam.Infrastructure.Local.TestData;
using Xunit;

namespace SteamAccountManager.Tests.Steam.Application.UseCase
{
    public class SwitchAccountUseCaseTest
    {
        private readonly Mock<ISteamRepository> _steamRepositoryMock;
        private readonly Mock<ISteamProcessService> _steamProcessServiceMock;
        private readonly Mock<ILogger> _loggerMock;
        private readonly ISwitchAccountUseCase _sut;

        public SwitchAccountUseCaseTest()
        {
            _steamRepositoryMock = new Mock<ISteamRepository>(behavior: MockBehavior.Strict);
            _steamProcessServiceMock = new Mock<ISteamProcessService>(behavior: MockBehavior.Strict);
            _loggerMock = new Mock<ILogger>(behavior: MockBehavior.Loose);
            _sut = new SwitchAccountUseCase(
                _steamRepositoryMock.Object,
                _steamProcessServiceMock.Object,
                _loggerMock.Object
            );
        }

        [Fact]
        public async void SwitchAccount_catches_UpdateAutoLoginUserFailedException()
        {
            var samFisherLoginUser = SteamUserTestData.GetSamFisherLoginUser();

            _steamRepositoryMock.Setup(repo => repo.UpdateAutoLoginUser(samFisherLoginUser.AccountName))
                .Throws<UpdateAutoLoginUserFailedException>();

            await _sut.Execute(samFisherLoginUser.AccountName);
        }

        [Fact]
        public async void SwitchAccount_returns_false_UpdateAutoLoginUserFailedException_is_thrown()
        {
            var samFisherLoginUser = SteamUserTestData.GetSamFisherLoginUser();

            _steamRepositoryMock.Setup(repo => repo.UpdateAutoLoginUser(samFisherLoginUser.AccountName))
                .Throws<UpdateAutoLoginUserFailedException>();

            Assert.False(await _sut.Execute(samFisherLoginUser.AccountName));
        }
    }
}