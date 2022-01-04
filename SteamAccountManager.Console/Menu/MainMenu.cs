using Autofac;
using SteamAccountManager.Application.Steam.Service;
using System.Text;

namespace SteamAccountManager.Console.Menu
{
    public class MainMenu : IMenu
    {
        private readonly ISteamService _steamService;

        public MainMenu()
        {
            _steamService = Program.Container.Resolve<ISteamService>();

            Show();
        }

        public void Show()
        {
            ShowAccountSelection();
        }

        private void ShowAccountSelection()
        {
            var steamAccounts = _steamService.GetAccounts().Result;

            for (int i = 0; i < steamAccounts.Count; i++)
            {
                var account = steamAccounts[i];
                var accountDetails = new StringBuilder()
                    .Append($"{account.AccountName} |")
                    .Append($"| {account.Username} ")
                    .Append($"[Valid: {account.IsLoginValid}] ")
                    .Append($"[VAC: { account.IsVacBanned}] ")
                    .Append($"[Community Ban: {account.IsCommunityBanned}]")
                    .ToString();

                System.Console.WriteLine($"{i}. {accountDetails}");
            }

            System.Console.WriteLine("Enter Number to log in account, Habibi!!");

            string? accountSelection = System.Console.ReadLine();

            if (int.TryParse(accountSelection, out int accountIndex))
            {
                var selectedAccount = steamAccounts[accountIndex];
                System.Console.WriteLine($"Selected Account: {selectedAccount.AccountName}");

                _steamService.SwitchAccount(selectedAccount.AccountName);
            }

            OnAccountSelected();
        }

        private void OnAccountSelected()
        {
            ShowAccountSelection();
        }
    }
}