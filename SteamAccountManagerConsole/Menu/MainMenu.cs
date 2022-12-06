using System;
using System.Collections.Generic;
using Autofac;
using SteamAccountManager.Domain.Steam.Local.POCO;
using SteamAccountManager.Domain.Steam.Service;

namespace SteamAccountManagerConsole.Menu
{
    public class MainMenu : IMenu   
    {
        private readonly ISteamService _steamService;
        private List<SteamLoginUser> SteamAccounts { get; }

        public MainMenu()
        {
            _steamService = Program.Container.Resolve<ISteamService>();
            SteamAccounts = _steamService.GetAccounts().Result;

            Show();
        }
        
        public void Show()
        {
            ShowAccountSelection();
        }

        private void ShowAccountSelection()
        {
            Console.Clear();

            for (int i = 0; i < SteamAccounts.Count; i++)
            {
                var account = SteamAccounts[i];
                Console.WriteLine($"{i}. [Valid: {account.IsLoginTokenValid}] {account.AccountName}");
            }

            Console.WriteLine("Enter Number to log in account, Habibi!!");

            string? accountSelection = Console.ReadLine();
        
            if (Int32.TryParse(accountSelection, out int accountIndex))
            {
                var selectedAccount = SteamAccounts[accountIndex];
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