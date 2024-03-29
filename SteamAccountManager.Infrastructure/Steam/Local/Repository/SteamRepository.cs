﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SteamAccountManager.Domain.Steam.Exceptions;
using SteamAccountManager.Domain.Steam.Local.Logger;
using SteamAccountManager.Domain.Steam.Local.Repository;
using SteamAccountManager.Domain.Steam.Model;
using SteamAccountManager.Infrastructure.Steam.Local.DataSource;
using SteamAccountManager.Infrastructure.Steam.Local.Mapping;

namespace SteamAccountManager.Infrastructure.Steam.Local.Repository
{
    public class SteamRepository : ISteamRepository
    {
        private readonly ILocalSteamDataSource _steamDataSource;
        private readonly ILogger _logger;

        public SteamRepository(ILocalSteamDataSource steamDataSource, ILogger logger)
        {
            _steamDataSource = steamDataSource;
            _logger = logger;
        }

        public async Task<List<LoginUser>> GetSteamLoginHistoryUsers()
        {
            return (await _steamDataSource.GetUsersFromLoginHistory())
                .ToSteamLoginUsers();
        }

        public void UpdateAutoLoginUser(string accountName)
        {
            _steamDataSource.UpdateAutoLoginUser(accountName);
        }

        public async Task<LoginUser?> GetCurrentAutoLoginUser()
        {
            var currentAutoLoginAccountName = _steamDataSource.GetCurrentAutoLoginUser();
            try
            {
                var currentSteamLoginUser = (await _steamDataSource.GetUsersFromLoginHistory())
                    .FirstOrDefault(dto => dto.AccountName == currentAutoLoginAccountName)?
                    .ToSteamLoginUser();

                return currentSteamLoginUser;
            }
            catch (Exception e)
            {
                var exception = new SteamAutoLoginUserNotFoundException("Steam Auto Login User not found", e);
                _logger.LogException("User not found", exception);

                throw exception;
            }
        }
    }
}