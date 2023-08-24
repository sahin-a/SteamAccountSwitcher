using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using SteamAccountManager.Domain.Steam.Model;
using SteamAccountManager.Domain.Steam.UseCase;

namespace SteamAccountManager.AvaloniaUI.DemoMock
{
    internal class GetAccountWithDetailsUseCaseMock : IGetAccountsWithDetailsUseCase
    {
#pragma warning disable CS1998 // Bei der asynchronen Methode fehlen "await"-Operatoren. Die Methode wird synchron ausgeführt.
        public async Task<List<Account>> Execute()
#pragma warning restore CS1998 // Bei der asynchronen Methode fehlen "await"-Operatoren. Die Methode wird synchron ausgeführt.
        {
            return new List<Account>()
            {
                new()
                {
                    Id = "0",
                    Name = "scumbuster",
                    Username = "Imperial Guard",
                    AvatarUrl =
                        "https://pm1.aminoapps.com/6793/e363a3a87430bed557f2fcf5c6c0c49f64abc582v2_00.jpg",
                    Level = 999,
                    LastLogin = DateTime.Parse("2022-02-21 13:26"),
                    IsVacBanned = true
                },
                new()
                {
                    Id = "1",
                    Name = "lookingforsunderland",
                    Username = "Pyramid Head",
                    AvatarUrl =
                        "https://i.redd.it/qfdnsaaherq21.jpg",
                    Level = 111,
                    LastLogin = DateTime.UtcNow
                },
                new()
                {
                    Id = "43241",
                    Name = "romanbellic",
                    Username = "BowlingManLC",
                    AvatarUrl =
                        "https://static.wikia.nocookie.net/rockstargamescompany/images/a/a3/Roman_Bellic.jpg/revision/latest?cb=20130122105711",
                    Level = 222,
                    LastLogin = DateTime.UtcNow,
                    IsLoggedIn = true
                },
                new()
                {
                    Id = "2",
                    Name = "niko_bellic1978",
                    Username = "NikoLiberty",
                    AvatarUrl = "https://pbs.twimg.com/profile_images/1373612243888218115/INPHOoZK_400x400.jpg",
                    Level = 60,
                    LastLogin = DateTime.Parse("2011-03-21 13:26")
                },
                new()
                {
                    Id = "0",
                    Name = "spongebob_1999",
                    Username = "SpongeBob",
                    AvatarUrl = "https://thypix.com/wp-content/uploads/2021/11/sponge-bob-profile-picture-thypix-m.jpg",
                    Level = 25,
                    LastLogin = DateTime.Parse("2022-02-21 13:26"),
                    IsVacBanned = true
                },
                new()
                {
                    Id = "1",
                    Name = "sam_fisher",
                    Username = "Mr. Fisher",
                    AvatarUrl =
                        "https://steamuserimages-a.akamaihd.net/ugc/780749025467440214/E0244A02E538EA1287241F3F5C97DB5DA9748906/",
                    Level = 808,
                    LastLogin = DateTime.UtcNow
                },
                new()
                {
                    Id = "2",
                    Name = "niko_bellic1978",
                    Username = "NikoLiberty",
                    AvatarUrl = "https://pbs.twimg.com/profile_images/1373612243888218115/INPHOoZK_400x400.jpg",
                    Level = 255,
                    LastLogin = DateTime.Parse("2011-03-21 13:26")
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