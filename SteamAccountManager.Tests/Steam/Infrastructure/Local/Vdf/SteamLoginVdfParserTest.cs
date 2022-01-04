using Moq;
using SteamAccountManager.Application.Steam.Local.Logger;
using SteamAccountManager.Domain.Steam.Exception.Vdf;
using SteamAccountManager.Infrastructure.Steam.Local.Vdf;
using Xunit;

namespace SteamAccountManager.Tests.Steam.Infrastructure.Local.Vdf
{
    public class SteamLoginVdfParserTest
    {
        private const string ValidLoginUsersVdf =
            "\"users\"\r\n{\r\n\t\"646324523432\"\r\n\t{\r\n\t\t\"AccountName\"\t\t\"DieterSteamAccount\"\r\n\t\t\"PersonaName\"\t\t\"dieterApfelNickname\"\r\n\t\t\"RememberPassword\"\t\t\"1\"\r\n\t\t\"MostRecent\"\t\t\"1\"\r\n\t\t\"Timestamp\"\t\t\"1635552555\"\r\n\t}\r\n\t\"45674374567\"\r\n\t{\r\n\t\t\"AccountName\"\t\t\"ApfelsalatPeter\"\r\n\t\t\"PersonaName\"\t\t\"peternussNickname\"\r\n\t\t\"RememberPassword\"\t\t\"0\"\r\n\t\t\"MostRecent\"\t\t\"0\"\r\n\t\t\"Timestamp\"\t\t\"1627750152\"\r\n\t}\r\n}\r\n";

        private readonly Mock<ILogger> _logger;
        private readonly ISteamLoginVdfParser _sut;

        public SteamLoginVdfParserTest()
        {
            _logger = new Mock<ILogger>(behavior: MockBehavior.Loose);
            _sut = new SteamLoginVdfParser(_logger.Object);
        }

        [Fact]
        public void Parses_Valid_Login_Vdf_Correctly()
        {
            var loginUsers = _sut.ParseLoginUsers(ValidLoginUsersVdf);

            Assert.True(loginUsers.Count == 2);

            var firstAccount = loginUsers[0];
            var secondAccount = loginUsers[1];

            Assert.Equal("646324523432", firstAccount.SteamId);
            Assert.Equal("DieterSteamAccount", firstAccount.AccountName);
            Assert.Equal("dieterApfelNickname", firstAccount.PersonaName);
            Assert.Equal("1635552555", firstAccount.Timestamp);
            Assert.True(firstAccount.MostRecent);
            Assert.True(firstAccount.PasswordRemembered);

            Assert.Equal("45674374567", secondAccount.SteamId);
            Assert.Equal("ApfelsalatPeter", secondAccount.AccountName);
            Assert.Equal("peternussNickname", secondAccount.PersonaName);
            Assert.Equal("1627750152", secondAccount.Timestamp);
            Assert.False(secondAccount.MostRecent);
            Assert.False(secondAccount.PasswordRemembered);
        }

        [Fact]
        public void Throws_Exception_If_Invalid_Vdf()
        {
            Assert.Throws<InvalidVdfException>(() => _sut.ParseLoginUsers("apfelsalat"));
        }
    }
}