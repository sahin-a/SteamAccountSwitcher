using SteamAccountManager.Infrastructure.Steam.Local.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamAccountManager.Tests.Steam.Data.Local.TestData
{
    public static class VdfTestData
    {
        public static readonly string ValidLoginUsersVdf =
                    "\"users\"\r\n{\r\n\t\"646324523432\"\r\n\t{\r\n\t\t\"AccountName\"\t\t\"DieterSteamAccount\"\r\n\t\t\"PersonaName\"\t\t\"dieterApfelNickname\"\r\n\t\t\"RememberPassword\"\t\t\"1\"\r\n\t\t\"MostRecent\"\t\t\"1\"\r\n\t\t\"Timestamp\"\t\t\"1635552555\"\r\n\t}\r\n\t\"45674374567\"\r\n\t{\r\n\t\t\"AccountName\"\t\t\"ApfelsalatPeter\"\r\n\t\t\"PersonaName\"\t\t\"peternussNickname\"\r\n\t\t\"RememberPassword\"\t\t\"0\"\r\n\t\t\"MostRecent\"\t\t\"0\"\r\n\t\t\"Timestamp\"\t\t\"1627750152\"\r\n\t}\r\n}\r\n";

        public static readonly LoginUserDto loginUserDto1 = new LoginUserDto()
        {
            SteamId = "1337",
            AccountName = "Hallo",
            MostRecent = true,
            PersonaName = "Welt",
            PasswordRemembered = true,
            Timestamp = "312523534"
        };
    }
}
