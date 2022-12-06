using SteamAccountManager.AvaloniaUI.Common;
using SteamAccountManager.AvaloniaUI.Common.Utils;
using SteamAccountManager.AvaloniaUI.Models;
using SteamAccountManager.AvaloniaUI.Services;
using System;
using System.Text;
using System.Threading.Tasks;
using Account = SteamAccountManager.AvaloniaUI.Models.Account;

namespace SteamAccountManager.AvaloniaUI.Mappers
{
    internal class AccountMapper
    {
        private AvatarService _avatarService;

        public AccountMapper(AvatarService avatarService)
        {
            _avatarService = avatarService;
        }

        private string GetTimePassedFormatted(int minutesPassed)
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

        public async Task<Account> FromSteamAccount(Domain.Steam.Model.Account steamAccount)
        {
            var minutesPassed = Convert.ToUInt16(DateTime.UtcNow.Subtract(steamAccount.LastLogin).TotalMinutes);
            var timePassedSinceLastLogin = GetTimePassedFormatted(minutesPassed);
            var rank = new Rank
            {
                Level = steamAccount.Level
            };

            var account = new Account
            {
                SteamId = steamAccount.Id,
                Name = steamAccount.Name,
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
    }
}
