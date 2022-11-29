using SteamAccountManager.Application.Steam.Exceptions;
using SteamAccountManager.Application.Steam.Local.Logger;
using SteamAccountManager.Application.Steam.Local.Repository;
using SteamAccountManager.Application.Steam.Service;

namespace SteamAccountManager.Application.Steam.UseCase
{
    public interface ISwitchAccountUseCase
    {
        public Task<bool> Execute(string accountName);
    }

    public class SwitchAccountUseCase : ISwitchAccountUseCase
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
