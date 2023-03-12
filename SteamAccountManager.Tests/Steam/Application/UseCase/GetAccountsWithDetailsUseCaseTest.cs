using Moq;
using SteamAccountManager.Domain.Steam.Model;
using SteamAccountManager.Tests.Steam.Infrastructure.Local.TestData;
using System.Collections.Generic;
using SteamAccountManager.Domain.Steam.Local.Logger;
using SteamAccountManager.Domain.Steam.Local.Repository;
using SteamAccountManager.Domain.Steam.Service;
using SteamAccountManager.Domain.Steam.UseCase;
using Xunit;

namespace SteamAccountManager.Tests.Steam.Application.UseCase
{
    public class GetAccountsWithDetailsUseCaseTest
    {
        private readonly Mock<ISteamRepository> _steamRepositoryMock;
        private readonly Mock<ISteamProfileService> _steamProfileServiceMock;
        private readonly Mock<ILogger> _loggerMock;
        private readonly GetAccountsWithDetailsUseCase _sut;

        public GetAccountsWithDetailsUseCaseTest()
        {
            _steamRepositoryMock = new Mock<ISteamRepository>(behavior: MockBehavior.Strict);
            _steamProfileServiceMock = new Mock<ISteamProfileService>(behavior: MockBehavior.Strict);
            _loggerMock = new Mock<ILogger>(behavior: MockBehavior.Loose);
            _sut = new GetAccountsWithDetailsUseCase(
                _steamRepositoryMock.Object,
                _steamProfileServiceMock.Object,
                _loggerMock.Object
            );
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
                    new List<LoginUser>
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
                    new List<Profile>
                    {
                        samFisherProfile,
                        rainerProfile
                    }
                )
                .Verifiable();

            var result = await _sut.Execute();

            var samFisherAccount = ToAccount(samFisherLoginUser, samFisherProfile);
            var rainerAccount = ToAccount(rainerLoginUser, rainerProfile);

            AssertSteamAccount(expected: samFisherAccount, actual: result[0]);
            AssertSteamAccount(expected: rainerAccount, actual: result[1]);
        }

        private Account ToAccount(LoginUser loginUser, Profile profile) => new Account
        {
            Id = loginUser.SteamId,
            Name = loginUser.AccountName,
            Username = profile.Username,
            AvatarUrl = profile.AvatarUrl,
            ProfileUrl = profile.Url,
            IsCommunityBanned = profile.IsCommunityBanned,
            IsVacBanned = profile.IsVacBanned,
            IsLoginValid = loginUser.IsLoginTokenValid,
            LastLogin = loginUser.LastLogin,
            Level = profile.Level
        };

        private void AssertSteamAccount(Account expected, Account actual)
        {
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.Username, actual.Username);
            Assert.Equal(expected.AvatarUrl, actual.AvatarUrl);
            Assert.Equal(expected.ProfileUrl, actual.ProfileUrl);
            Assert.Equal(expected.IsLoginValid, actual.IsLoginValid);
        }
    }
}
