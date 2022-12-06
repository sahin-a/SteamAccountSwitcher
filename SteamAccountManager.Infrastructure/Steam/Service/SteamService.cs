using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SteamAccountManager.Domain.Steam.Local.POCO;
using SteamAccountManager.Domain.Steam.Local.Repository;
using SteamAccountManager.Domain.Steam.Service;

namespace SteamAccountManager.Infrastructure.Steam.Service
{
    public class SteamService : ISteamService
    {
        private readonly ISteamRepository _steamRepository;

        public SteamService(ISteamRepository steamRepository)
        {
            _steamRepository = steamRepository;
        }

        public async Task<List<SteamLoginUser>> GetAccounts()
        {
            try
            {
                return await _steamRepository.GetSteamLoginUsers();
            }
            catch (Exception e)
            {
                return new List<SteamLoginUser>();
            }
        }

        public bool LogInAccount(string accountName)
        {
            try
            {
                _steamRepository.UpdateAutoLoginUser(accountName);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}