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
        private AvatarService _avatarService;

        public AccountSwitcherViewModel(ISteamService steamService, AvatarService imageProvider)
        {
            _steamService = steamService;
            _avatarService = imageProvider;
            Accounts = new ObservableCollection<Account>();
            ProfileClickedCommand = new ProfileClickedCommand();

            LoadAccounts();
        }

        // TODO: refactor else :vomit:
        public async Task<Account> ToAccount(SteamAccount steamAccount) => new Account
        {
            SteamId = steamAccount.SteamId,
            Name = steamAccount.AccountName,
            ProfilePicture = await _avatarService.GetAvatarAsync(steamAccount.AvatarUrl),
            Username = steamAccount.Username,
            ProfileUrl = steamAccount.ProfileUrl,
            IsVacBanned = steamAccount.IsVacBanned,
            IsCommunityBanned = steamAccount.IsCommunityBanned
        };

        public async void LoadAccounts()
        {
            Accounts.Clear();

            var steamAccounts = await _steamService.GetAccounts();

            steamAccounts.ForEach(async account => Accounts.Add(await ToAccount(account)));
        }

        public void OnAccountSelected(Account selectedAccount)
        {
            _steamService.SwitchAccount(selectedAccount.Name);
        }
    }
}
