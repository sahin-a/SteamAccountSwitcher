using SteamAccountManager.Domain.Steam.Local.Logger;
using SteamAccountManager.Domain.Steam.Service;
using SteamAccountManager.Infrastructure.Steam.Local.Dao;
using System;
using System.Diagnostics;
using System.Linq;

namespace SteamAccountManager.Infrastructure.Steam.Service
{
    public class SteamProcessService : ISteamProcessService
    {
        private readonly ISteamConfig _steamConfig;
        private readonly ILogger _logger;

        public SteamProcessService(ISteamConfig steamConfig, ILogger logger)
        {
            _steamConfig = steamConfig;
            _logger = logger;
        }

        public bool KillSteam()
        {
            var steamProcess = Process.GetProcessesByName("Steam")
                .FirstOrDefault();

            if (steamProcess == null)
                return true;

            // might be not nescessary bruh
            try
            {
                steamProcess.Kill(entireProcessTree: true);
                return steamProcess.WaitForExit(5000);
            }
            catch (Exception e)
            {
                _logger.LogException(GetType().Name, "Failed to kill steam process", e);
                return false;
            }
        }

        public void StartSteam()
        {
            try
            {
                Process.Start(_steamConfig.GetSteamExecutablePath());
            }
            catch (Exception e)
            {
                _logger.LogException(GetType().Name, "Failed to start steam :(", e);
            }
        }
    }
}
