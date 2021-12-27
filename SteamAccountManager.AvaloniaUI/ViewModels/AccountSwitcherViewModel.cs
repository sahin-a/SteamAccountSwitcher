using Avalonia;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using SteamAccountManager.Application.Steam.Model;
using SteamAccountManager.Application.Steam.Service;
using SteamAccountManager.AvaloniaUI.Models;
using SteamAccountManager.AvaloniaUI.Services;
using SteamAccountManager.AvaloniaUI.ViewModels.Commands;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SteamAccountManager.AvaloniaUI.ViewModels
{
    // TODO: looks ridicilous, I should refactor all of this but I don't feel bored enough yet
    internal class AccountSwitcherViewModel
    {
        private ISteamService _steamService;
        public ObservableCollection<Account> Accounts { get; }
        public ICommand ProfileClickedCommand { get; }
        public ICommand RefreshAccountsCommand { get; }
        public ICommand ShowInfoCommand { get; }
        private AvatarService _avatarService;

        public AccountSwitcherViewModel(ISteamService steamService, AvatarService imageProvider)
        {
            _steamService = steamService;
            _avatarService = imageProvider;
            Accounts = new ObservableCollection<Account>();
            ProfileClickedCommand = new ProfileClickedCommand();
            RefreshAccountsCommand = new QuickCommand(LoadAccounts);
            ShowInfoCommand = new QuickCommand(ShowInfo);

            LoadAccounts();
        }

        // TODO: get it out of this class
        public async Task<Account> ToAccount(SteamAccount steamAccount)
        {
            var hoursPassed = Convert.ToUInt16(DateTime.UtcNow.Subtract(steamAccount.LastLogin).TotalHours);
            var timePassedSinceLastLogin = (hoursPassed >= 24 ? $"{hoursPassed / 24} days" : $"{hoursPassed} hours") + " ago";

            var account = new Account
            {
                SteamId = steamAccount.SteamId,
                Name = steamAccount.AccountName,
                ProfilePicture = await _avatarService.GetAvatarAsync(steamAccount.AvatarUrl),
                Username = steamAccount.Username,
                ProfileUrl = steamAccount.ProfileUrl,
                IsVacBanned = steamAccount.IsVacBanned,
                IsCommunityBanned = steamAccount.IsCommunityBanned,
                LastLogin = timePassedSinceLastLogin,
                Level = steamAccount.Level
            };

            return account;
        }

        public async void LoadAccounts()
        {
            var steamAccounts = await _steamService.GetAccounts();
            var accounts = await Task.WhenAll(steamAccounts.ConvertAll(x => ToAccount(x)));

            foreach (var account in accounts)
            {
                // update entry if already exists
                var currentAccount = Accounts.Where(x => x.SteamId == account.SteamId).FirstOrDefault();
                if (currentAccount is Account)
                {
                    var index = Accounts.IndexOf(currentAccount);
                    Accounts[index] = account;
                    continue;
                }

                Accounts.Add(account);
            }
        }

        public void OnAccountSelected(Account selectedAccount)
        {
            _steamService.SwitchAccount(selectedAccount.Name);
        }

        public void ShowInfo()
        {
            System.Diagnostics.Process.Start("explorer", "https://github.com/sahin-a/SteamAccountManager/");
        }
    }
}
