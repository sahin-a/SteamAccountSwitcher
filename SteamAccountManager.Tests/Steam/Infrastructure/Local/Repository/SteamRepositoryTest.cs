using System.Collections.Generic;
using Moq;
using SteamAccountManager.Application.Steam.Local.Logger;
using SteamAccountManager.Application.Steam.Local.Repository;
using SteamAccountManager.Application.Steam.Model;
using SteamAccountManager.Domain.Steam.Exception;
using SteamAccountManager.Infrastructure.Steam.Local.DataSource;
using SteamAccountManager.Infrastructure.Steam.Local.Dto;
using SteamAccountManager.Infrastructure.Steam.Local.Repository;
using SteamAccountManager.Tests.Steam.Infrastructure.Local.TestData;
using Xunit;

namespace SteamAccountManager.Tests.Steam.Infrastructure.Local.Repository
{
    public class SteamRepositoryTest
    {
        private readonly Mock<ILocalSteamDataSource> _localSteamDataSourceMock;
        private readonly Mock<ILogger> _logger;
        private readonly ISteamRepository _sut;

        public SteamRepositoryTest()
        {
            _localSteamDataSourceMock = new Mock<ILocalSteamDataSource>(behavior: MockBehavior.Strict);
            _logger = new Mock<ILogger>(behavior: MockBehavior.Loose);
            _sut = new SteamRepository(_localSteamDataSourceMock.Object, _logger.Object);
        }

        [Fact]
        public async void Returns_correctly_converted_Steam_Login_Users()
        {
            var loginUserDtos = new List<LoginUserDto>
            {
                VdfTestData.loginUserDto1
            };

            _localSteamDataSourceMock.Setup(ds => ds.GetUsersFromLoginHistory())
                .ReturnsAsync(loginUserDtos);

            var users = await _sut.GetSteamLoginHistoryUsers();

            var loginUserDto = loginUserDtos[0];
            var steamLoginUser = users[0];

            Assert.Equal(expected: loginUserDto.SteamId, actual: steamLoginUser.SteamId);
            Assert.Equal(expected: loginUserDto.AccountName, actual: steamLoginUser.AccountName);
            Assert.Equal(expected: loginUserDto.PasswordRemembered, actual: steamLoginUser.IsLoginTokenValid);
        }

        [Fact]
        public async void Get_Steam_Login_Users_calls_Get_Logged_In_Users()
        {
            var loginUserDtos = new List<LoginUserDto>
            {
                VdfTestData.loginUserDto1
            };

            _localSteamDataSourceMock.Setup(ds => ds.GetUsersFromLoginHistory())
                .ReturnsAsync(loginUserDtos)
                .Verifiable();

            await _sut.GetSteamLoginHistoryUsers();

            _localSteamDataSourceMock.Verify(ds => ds.GetUsersFromLoginHistory(), Times.Once);
        }
        
        [Fact]
        public void UpdateAutoLoginUser_passes_correct_accountName()
        {
            string accountName = "Peter";
            var steamLoginUser = new SteamLoginUser.Builder()
                .SetSteamId("343242")
                .SetAccountName("Peter")
                .SetIsLoginTokenValid(true)
                .Build();

            _localSteamDataSourceMock.Setup(ds => ds.UpdateAutoLoginUser(accountName))
                .Verifiable();

            _sut.UpdateAutoLoginUser(steamLoginUser);

            _localSteamDataSourceMock.Verify(
                expression: ds => ds.UpdateAutoLoginUser(accountName),
                times: Times.Once
            );
        }
        
        [Fact]
        public void GetCurrentAutoLoginUser_calls_GetCurrentAutoLoginUser()
        {
            string accountName = "sahin";
            _localSteamDataSourceMock.Setup(ds => ds.GetCurrentAutoLoginUser())
                .Returns(accountName)
                .Verifiable();

            _sut.GetCurrentAutoLoginUser();

            _localSteamDataSourceMock.Verify(ds => ds.GetCurrentAutoLoginUser(), Times.Once);
        }

        [Fact]
        public async void GetCurrentAutoLoginUser_Returns_Correct_SteamLoginUser()
        {
            var loginUserDtos = new List<LoginUserDto>
            {
                VdfTestData.loginUserDto1
            };

            _localSteamDataSourceMock.Setup(ds => ds.GetUsersFromLoginHistory())
                .ReturnsAsync(loginUserDtos);

            _localSteamDataSourceMock.Setup(ds => ds.GetCurrentAutoLoginUser())
                .Returns(VdfTestData.loginUserDto1.AccountName);

            var expectedSteamLoginUser = new SteamLoginUser.Builder()
                .SetAccountName(VdfTestData.loginUserDto1.AccountName)
                .SetSteamId(VdfTestData.loginUserDto1.SteamId)
                .SetIsLoginTokenValid(VdfTestData.loginUserDto1.PasswordRemembered)
                .Build();

            var steamLoginUser = await _sut.GetCurrentAutoLoginUser();

            Assert.Equal(expected: expectedSteamLoginUser.SteamId, actual: steamLoginUser.SteamId);
            Assert.Equal(expected: expectedSteamLoginUser.AccountName, actual: steamLoginUser.AccountName);
            Assert.Equal(expected: expectedSteamLoginUser.IsLoginTokenValid, actual: steamLoginUser.IsLoginTokenValid);
        }
        
        [Fact]
        public void GetCurrentAutoLoginUser_Throws_Exception_If_User_Not_Found()
        {
            var loginUserDtos = new List<LoginUserDto>
            {
                VdfTestData.loginUserDto1
            };

            _localSteamDataSourceMock.Setup(ds => ds.GetUsersFromLoginHistory())
                .ReturnsAsync(loginUserDtos);

            _localSteamDataSourceMock.Setup(ds => ds.GetCurrentAutoLoginUser())
                .Returns("fsdf");

            var steamLoginUser = new SteamLoginUser.Builder()
                .SetAccountName("Peter Lol")
                .SetSteamId("42342352634")
                .SetIsLoginTokenValid(false)
                .Build();

            Assert.ThrowsAsync<SteamAutoLoginUserNotFoundException>(() =>
                _sut.GetCurrentAutoLoginUser()
            );
        }
    }
}