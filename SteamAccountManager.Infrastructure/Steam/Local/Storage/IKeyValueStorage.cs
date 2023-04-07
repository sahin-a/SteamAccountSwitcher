using System.Threading.Tasks;

namespace SteamAccountManager.Infrastructure.Steam.Local.Storage
{
    internal interface IKeyValueStorage<TValue> where TValue : class
    {
        public Task Store(string key, TValue value);
        public Task<TValue?> Get(string key, TValue? defaultValue = null);
    }
}