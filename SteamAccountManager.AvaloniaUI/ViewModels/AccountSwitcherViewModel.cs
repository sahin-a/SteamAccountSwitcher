using SteamAccountManager.Application.Steam.Model;
using SteamAccountManager.Application.Steam.Service;
using SteamAccountManager.AvaloniaUI.Common;
using SteamAccountManager.AvaloniaUI.Common.Utils;
using SteamAccountManager.AvaloniaUI.Models;
using SteamAccountManager.AvaloniaUI.Services;
using SteamAccountManager.AvaloniaUI.ViewModels.Commands;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
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

        public string GetTimePassedFormatted(int minutesPassed)
        {

            var stringBuilder = new StringBuilder();

            switch (minutesPassed)
            {
                case >= Time.DAY_IN_MINUTES:
                    stringBuilder.Append($"{TimeConverter.ToDays(minutesPassed)} days");
                    break;
                case >= Time.HOUR_IN_MINUTES:
                    stringBuilder.Append($"{TimeConverter.ToHours(minutesPassed)} hours");
                    break;
                default:
                    stringBuilder.Append($"{minutesPassed} minutes");
                    break;
            }
            stringBuilder.Append(" ago");

            return stringBuilder.ToString();
        }

        // TODO: get it out of this class
        public async Task<Account> ToAccount(SteamAccount steamAccount)
        {
            var minutesPassed = Convert.ToUInt16(DateTime.UtcNow.Subtract(steamAccount.LastLogin).TotalMinutes);
            var timePassedSinceLastLogin = GetTimePassedFormatted(minutesPassed);
            var rank = new Rank
            {
                Level = steamAccount.Level
            };

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
                Rank = rank
            };

            return account;
        }

        public async void LoadAccounts()
        {
            var steamAccounts = await _steamService.GetAccounts();
            var accounts = await Task.WhenAll(steamAccounts.ConvertAll(x => ToAccount(x)));

            foreach (var account in accounts)
            {
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
