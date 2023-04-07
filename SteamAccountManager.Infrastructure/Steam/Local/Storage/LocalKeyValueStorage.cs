using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SteamAccountManager.Domain.Steam.Local.Logger;
using SteamAccountManager.Infrastructure.Steam.Local.Dao;
using SteamAccountManager.Infrastructure.Steam.Local.DataSource;

namespace SteamAccountManager.Infrastructure.Steam.Local.Storage
{
    public abstract class LocalKeyValueStorage<TValue> : IKeyValueStorage<TValue> where TValue : class
    {
        private readonly SemaphoreSlim semaphoreSlim = new(1, 1);
        private readonly ObjectStorage<Dictionary<string, TValue>> _storage;

        protected LocalKeyValueStorage(ILogger logger, IFileProvider fileProvider, string name) : this(logger,
            new FileDataSource(directory: AppDataDirectory.Storages, fileProvider), name)
        {
        }

        private LocalKeyValueStorage(ILogger logger, IKeyValueDataSource dataSource,
            string name)
        {
            _storage = new ObjectStorage<Dictionary<string, TValue>>(name, logger, dataSource);
        }

        public async Task Store(string key, TValue value)
        {
            await semaphoreSlim.WaitAsync();

            Exception? exception = null;
            try
            {
                try
                {
                    var avatarMap = await _storage.Get() ?? new();
                    avatarMap[key] = value;

                    await _storage.Set(avatarMap);
                }
                catch (Exception e)
                {
                    exception = e;
                }
            }
            finally
            {
                semaphoreSlim.Release();

                if (exception is not null)
                    throw exception;
            }
        }

        public async Task<TValue?> Get(string key, TValue? defaultValue = null)
        {
            var avatarMap = await _storage.Get();
            return avatarMap?.TryGetValue(key, out var value) == true ? value : defaultValue;
        }
    }
}