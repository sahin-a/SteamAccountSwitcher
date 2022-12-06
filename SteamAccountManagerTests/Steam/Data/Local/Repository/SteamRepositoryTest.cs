using Moq;
using SteamAccountManager.Infrastructure.Steam.Local.DataSource;
using SteamAccountManager.Infrastructure.Steam.Local.Dto;
using SteamAccountManager.Infrastructure.Steam.Local.Repository;
using SteamAccountManager.Tests.Steam.Data.Local.TestData;
using System.Collections.Generic;
using Xunit;

namespace SteamAccountManager.Tests.Steam.Data.Local.Repository
{
    public class SteamRepositoryTest
    {
        private readonly Mock<ILocalSteamDataSource> _localSteamDataSource;
        private readonly SteamRepository _sut;

        public SteamRepositoryTest()
        {
            _localSteamDataSource = new Mock<ILocalSteamDataSource>(behavior: MockBehavior.Strict);
            _sut = new SteamRepository(_localSteamDataSource.Object);
        }

        [Fact]
        public async void Returns_correctly_converted_Steam_Login_Users()
        {
            var loginUserDtos = new List<LoginUserDto>
                {
                    VdfTestData.loginUserDto1
                };

            _localSteamDataSource.Setup(ds => ds.GetLoggedInUsers())
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

            _localSteamDataSource.Setup(ds => ds.GetLoggedInUsers())
                .ReturnsAsync(loginUserDtos)
                .Verifiable();

            await _sut.GetSteamLoginUsers();

            _localSteamDataSource.Verify(ds => ds.GetLoggedInUsers(), Times.Once);
        }
    }
}
