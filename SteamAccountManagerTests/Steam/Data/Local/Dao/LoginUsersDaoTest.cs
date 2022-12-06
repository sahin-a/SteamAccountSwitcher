using System.Collections.Generic;
using Moq;
using SteamAccountManager.Infrastructure.Steam.Local.Dao;
using SteamAccountManager.Infrastructure.Steam.Local.Dto;
using SteamAccountManager.Infrastructure.Steam.Local.Vdf;
using Xunit;

namespace SteamAccountManagerTests.Steam.Data.Local.Dao
{
    public class LoginUsersDaoTest
    {
        private readonly LoginUserDto _loginUserDto = new LoginUserDto()
        {
            SteamId = "1337",
            AccountName = "Hallo",
            MostRecent = true,
            PersonaName = "Welt",
            PasswordRemembered = true,
            Timestamp = "312523534"
        };
        
        /* Example */
        //private Mock<IGameDao> dao;

        // [Fact]
        // public void GetGames_Calls_GetGames_On_Dao()
        // {
        //     dao = new(behavior: MockBehavior.Strict);
        //     dataSource = new GamesDataSource(dao.Object);
        //
        //     dao.Setup(dao => dao.GetGames())
        //         .Returns(new List<GameEntity>())
        //         .Verifiable();
        //
        //     dataSource.GetGames();
        //
        //     dao.Verify(dao => dao.GetGames(), Times.Once);
        // }

        private readonly Mock<ISteamLoginVdfReader> _steamVdfReader;
        private readonly LoginUsersDao _sut;

        public LoginUsersDaoTest()
        {
            _steamVdfReader = new Mock<ISteamLoginVdfReader>(behavior: MockBehavior.Strict);
            _sut = new LoginUsersDao(_steamVdfReader.Object);
        }
        
        [Fact]
        public async void GetLoggedUsers_calls_ParseLoginUsersVdf()
        {
            _steamVdfReader.Setup(sr => sr.GetParsedLoginUsersVdf())
                .ReturnsAsync(new List<LoginUserDto> { _loginUserDto })
                .Verifiable();

            await _sut.GetLoggedUsers();
            
            _steamVdfReader.Verify(sr => sr.GetParsedLoginUsersVdf(), Times.Once);
        }
        
        [Fact]
        public async void Retrieve_Users_from_Login_Users_Vdf()
        {
            _steamVdfReader.Setup(sr => sr.GetParsedLoginUsersVdf())
                .ReturnsAsync(new List<LoginUserDto> { _loginUserDto, _loginUserDto });

            var loggedUsers = await _sut.GetLoggedUsers();
            Assert.True(loggedUsers.Count == 2);
        }
    }
}