using SteamAccountManager.Domain.Steam.Model;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace SteamAccountManager.AvaloniaUI.DemoMock
{
    internal class GetAccountWithDetailsUseCaseMock
    {
#pragma warning disable CS1998 // Bei der asynchronen Methode fehlen "await"-Operatoren. Die Methode wird synchron ausgeführt.
        public async Task<List<Account>> GetAccounts()
#pragma warning restore CS1998 // Bei der asynchronen Methode fehlen "await"-Operatoren. Die Methode wird synchron ausgeführt.
        {
            return new List<Account>()
            {
                new Account()
                {
                    Id = "0",
                    Name = "spongebob_1999",
                    Username = "SpongeBob",
                    AvatarUrl = "https://thypix.com/wp-content/uploads/2021/11/sponge-bob-profile-picture-thypix-m.jpg",
                    Level = 25,
                    LastLogin= System.DateTime.Parse("2022-02-21 13:26")
                },
                new Account()
                {
                    Id = "1",
                    Name = "sam_fisher",
                    Username = "Mr. Fisher",
                    AvatarUrl = "https://steamuserimages-a.akamaihd.net/ugc/780749025467440214/E0244A02E538EA1287241F3F5C97DB5DA9748906/",
                    Level = 111,
                    LastLogin = System.DateTime.UtcNow
                },
                new Account()
                {
                    Id = "2",
                    Name = "niko_bellic1978",
                    Username = "NikoLiberty",
                    AvatarUrl = "https://pbs.twimg.com/profile_images/1373612243888218115/INPHOoZK_400x400.jpg",
                    Level = 60,
                    LastLogin= System.DateTime.Parse("2011-03-21 13:26")
                }
            };
        }

        public bool SwitchAccount(string accountName)
        {
            Debug.WriteLine($"Switch to {accountName} requested");
            return true;
        }
    }
}
