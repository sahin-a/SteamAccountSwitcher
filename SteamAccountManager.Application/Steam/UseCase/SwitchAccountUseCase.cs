using SteamAccountManager.Application.Steam.Local.Logger;
using SteamAccountManager.Application.Steam.Local.Repository;
using SteamAccountManager.Application.Steam.Service;
using SteamAccountManager.Domain.Steam.Exception;

namespace SteamAccountManager.Application.Steam.UseCase
{
    public class SwitchAccountUseCase
    {
        private readonly ISteamRepository _steamRepository;
        private readonly ISteamProcessService _steamProcessService;
        private readonly ILogger _logger;

        public SwitchAccountUseCase(ISteamRepository steamRepository, ISteamProcessService steamProcessService, ILogger logger)
        {
            _steamRepository = steamRepository;
            _steamProcessService = steamProcessService;
            _logger = logger;
        }

        public async Task<bool> Execute(string accountName)
        {
            try
            {
                _steamRepository.UpdateAutoLoginUser(accountName);
            }
            catch (UpdateAutoLoginUserFailedException e)
            {
                _logger.LogException("Failed to update autologin account :(", e);
                return false;
            }

            if (_steamProcessService.KillSteam())
                await _steamProcessService.StartSteam();

            return true;
        }
    }
}
