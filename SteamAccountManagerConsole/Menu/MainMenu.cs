using System;
using Autofac;
using SteamAccountManager.Domain.Steam.Service;

namespace SteamAccountManagerConsole.Menu
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
            Console.Clear();

            var steamAccounts = _steamService.GetAccounts().Result;
            
            for (int i = 0; i < steamAccounts.Count; i++)
            {
                var account = steamAccounts[i];
                Console.WriteLine($"{i}. [Valid: {account.IsLoginTokenValid}] {account.AccountName}");
            }

            Console.WriteLine("Enter Number to log in account, Habibi!!");

            string? accountSelection = Console.ReadLine();
        
            if (Int32.TryParse(accountSelection, out int accountIndex))
            {
                var selectedAccount = steamAccounts[accountIndex];
                Console.WriteLine($"Selected Account: {selectedAccount.AccountName}");
                
                _steamService.SwitchAccount(selectedAccount);
            }

            OnAccountSelected();
        }

        private void OnAccountSelected()
        {
            ShowAccountSelection();
        }
    }
}