using System.Collections.Generic;
using System.Threading.Tasks;
using SteamAccountManager.Domain.Steam.Local.Logger;
using SteamAccountManager.Infrastructure.Steam.Local.Dao;
using SteamAccountManager.Infrastructure.Steam.Local.DataSource;

namespace SteamAccountManager.Infrastructure.Steam.Local.Storage
{
    public abstract class LocalKeyValueStorage<TValue> : ObjectStorage<Dictionary<string, TValue>>,
        IKeyValueStorage<TValue> where TValue : class
    {
        protected LocalKeyValueStorage(ILogger logger, IFileProvider fileProvider, string name) : this(logger,
            new FileDataSource(directory: AppDataDirectory.Storages, fileProvider), name)
        {
        }

        private LocalKeyValueStorage(ILogger logger, IKeyValueDataSource dataSource,
            string name) : base(name, logger, dataSource)
        {
        }

        protected override Dictionary<string, TValue> GetDefaultValue() => new();

        public async Task Store(string key, TValue value)
        {
            var avatarMap = await base.Get();
            avatarMap![key] = value;

            await Set(avatarMap);
        }

        public async Task<TValue?> Get(string key, TValue? defaultValue = null)
        {
            var avatarMap = await base.Get();
            return avatarMap!.TryGetValue(key, out var value) ? value : defaultValue;
        }
    }
}