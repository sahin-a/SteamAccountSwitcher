using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace SteamAccountManager.Infrastructure.Steam.Local.Storage
{
    public abstract class LocalKeyValueStorage<TValue> : IKeyValueDataSource<TValue> where TValue : class
    {
        private readonly object _lock = new object();
        private readonly string _fileName;
        private readonly Dictionary<string, TValue> _keyValuePairs;

        public LocalKeyValueStorage(string name)
        {
            _fileName = Path.ChangeExtension(name, ".json");
            _keyValuePairs = GetInitialValue();
        }

        private Dictionary<string, TValue> GetInitialValue()
        {
            if (!File.Exists(_fileName))
                return new();

            try
            {
                using (var sr = new StreamReader(_fileName))
                {
                    return JsonConvert.DeserializeObject<Dictionary<string, TValue>>(sr.ReadToEnd());
                }
            }
            catch (Exception)
            {

            }

            return new();
        }

        private void Save()
        {
            lock (_lock)
            {
                using (var sw = new StreamWriter(_fileName))
                {
                    sw.WriteLine(JsonConvert.SerializeObject(_keyValuePairs, Formatting.Indented));
                }
            }
        }

        public void Store(string key, TValue value)
        {
            _keyValuePairs[key] = value;
            Save();
        }

        public TValue? Get(string key, TValue? defaultValue = null)
        {
            return _keyValuePairs.GetValueOrDefault(key, defaultValue);
        }
    }
}
