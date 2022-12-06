using Moq;
using SteamAccountManager.Infrastructure.Steam.Local.DataSource;
using SteamAccountManager.Infrastructure.Steam.Local.Dto;
using SteamAccountManager.Infrastructure.Steam.Local.Repository;
using SteamAccountManager.Tests.Steam.Data.Local.TestData;
using System.Collections.Generic;
using SteamAccountManager.Domain.Steam.Local.POCO;
using Xunit;

namespace SteamAccountManager.Tests.Steam.Data.Local.Repository
{
    public class SteamRepositoryTest
    {
        private readonly Mock<ILocalSteamDataSource> _localSteamDataSourceMock;
        private readonly SteamRepository _sut;

        public SteamRepositoryTest()
        {
            _localSteamDataSourceMock = new Mock<ILocalSteamDataSource>(behavior: MockBehavior.Strict);
            _sut = new SteamRepository(_localSteamDataSourceMock.Object);
        }

        [Fact]
        public async void Returns_correctly_converted_Steam_Login_Users()
        {
            var loginUserDtos = new List<LoginUserDto>
            {
                VdfTestData.loginUserDto1
            };

            _localSteamDataSourceMock.Setup(ds => ds.GetLoggedInUsers())
                .ReturnsAsync(loginUserDtos);

            var users = await _sut.GetSteamLoginUsers();

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

            _localSteamDataSourceMock.Setup(ds => ds.GetLoggedInUsers())
                .ReturnsAsync(loginUserDtos)
                .Verifiable();

            await _sut.GetSteamLoginUsers();

            _localSteamDataSourceMock.Verify(ds => ds.GetLoggedInUsers(), Times.Once);
        }

        // TODO: add tests for get current autologin user
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
    }
}