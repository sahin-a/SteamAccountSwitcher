namespace SteamAccountManager.Infrastructure.Steam.Local.Storage
{
    internal interface IKeyValueDataSource<TValue> where TValue : class
    {
        public void Store(string key, TValue value);
        public TValue? Get(string key);
    }
}
