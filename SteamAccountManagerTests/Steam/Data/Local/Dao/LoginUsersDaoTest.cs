using System.Collections.Generic;
using Moq;
using SteamAccountManager.Infrastructure.Steam.Local.Dao;
using SteamAccountManager.Infrastructure.Steam.Local.Dto;
using SteamAccountManager.Infrastructure.Steam.Local.Vdf;
using SteamAccountManager.Tests.Steam.Data.Local.TestData;
using Xunit;

namespace SteamAccountManager.Tests.Steam.Data.Local.Dao
{
    public class LoginUsersDaoTest
    {
        private readonly Mock<ISteamLoginVdfReader> _steamVdfReader;
        private readonly Mock<ISteamLoginVdfParser> _steamVdfParser;
        private readonly Mock<ISteamLoginVdfWriter> _steamVdfWriter;
        private readonly LoginUsersDao _sut;

        public LoginUsersDaoTest()
        {
            _steamVdfReader = new Mock<ISteamLoginVdfReader>(behavior: MockBehavior.Strict);
            _steamVdfParser = new Mock<ISteamLoginVdfParser>(behavior: MockBehavior.Strict);
            _steamVdfWriter = new Mock<ISteamLoginVdfWriter>(behavior: MockBehavior.Strict);
            
            _sut = new LoginUsersDao(
                _steamVdfReader.Object,
                _steamVdfParser.Object,
                _steamVdfWriter.Object
                );
        }

        [Fact]
        public async void GetLoggedUsers_calls_ParseLoginUsersVdf()
        {
            _steamVdfReader.Setup(sr => sr.GetLoginUsersVdfContent())
                .ReturnsAsync(VdfTestData.ValidLoginUsersVdf)
                .Verifiable();

            _steamVdfParser.Setup(sp => sp.ParseLoginUsers(VdfTestData.ValidLoginUsersVdf))
                .Returns(new List<LoginUserDto> { VdfTestData.loginUserDto1 })
                .Verifiable();

            await _sut.GetLoggedUsers();

            _steamVdfParser.Verify(sp => sp.ParseLoginUsers(VdfTestData.ValidLoginUsersVdf), Times.Once);
        }

        [Fact]
        public async void Retrieve_Users_from_Login_Users_Vdf()
        {
            _steamVdfReader.Setup(sr => sr.GetLoginUsersVdfContent())
                .ReturnsAsync(VdfTestData.ValidLoginUsersVdf)
                .Verifiable();

            _steamVdfParser.Setup(sr => sr.ParseLoginUsers(VdfTestData.ValidLoginUsersVdf))
                .Returns(new List<LoginUserDto> { VdfTestData.loginUserDto1, VdfTestData.loginUserDto1 });

            var loggedUsers = await _sut.GetLoggedUsers();
            Assert.True(loggedUsers.Count == 2);
        }
    }
}