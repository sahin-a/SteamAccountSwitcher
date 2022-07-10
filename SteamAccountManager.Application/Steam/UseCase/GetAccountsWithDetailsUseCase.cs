using SteamAccountManager.Application.Steam.Local.Logger;
using SteamAccountManager.Application.Steam.Local.Repository;
using SteamAccountManager.Application.Steam.Model;
using SteamAccountManager.Application.Steam.Service;
using SteamAccountManager.Domain.Steam.Model;

namespace SteamAccountManager.Application.Steam.UseCase
{
    public class GetAccountsWithDetailsUseCase : IGetAccountsWithDetailsUseCase
    {
        private readonly ISteamRepository _steamRepository;
        private readonly ISteamProfileService _steamProfileService;
        private readonly ILogger _logger;

        public GetAccountsWithDetailsUseCase(ISteamRepository steamRepository, ISteamProfileService steamProfileService, ILogger logger)
        {
            _steamRepository = steamRepository;
            _steamProfileService = steamProfileService;
            _logger = logger;
        }

        public async Task<List<Account>> Execute()
        {
            try
            {
                var steamLoginUsers = await _steamRepository.GetSteamLoginHistoryUsers();
                var steamIds = steamLoginUsers.Select(user => user.SteamId);
                var steamProfiles = await _steamProfileService.GetProfileDetails(steamIds.ToArray());

                var steamAccounts = steamLoginUsers.ConvertAll(steamLoginUser =>
                {
                    var steamProfile = steamProfiles.FirstOrDefault(
                        profile => profile.Id == steamLoginUser.SteamId,
                        new Profile()
                        {
                            Username = steamLoginUser.Username,
                            Level = -1
                        }
                    );

                    return new Account()
                    {
                        Id = steamLoginUser.SteamId,
                        Name = steamLoginUser.AccountName,
                        Username = steamProfile.Username,
                        AvatarUrl = steamProfile.AvatarUrl,
                        ProfileUrl = steamProfile.Url,
                        IsVacBanned = steamProfile.IsVacBanned,
                        IsCommunityBanned = steamProfile.IsCommunityBanned,
                        LastLogin = steamLoginUser.LastLogin,
                        IsLoginValid = steamLoginUser.IsLoginTokenValid,
                        Level = steamProfile.Level
                    };
                });

                return steamAccounts;
            }
            catch (Exception e)
            {
                _logger.LogException("Failed to retrieve steam profile data", e);
            }

            return new List<Account>();
        }
    }
}
