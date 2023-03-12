﻿using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using SteamAccountManager.Domain.Steam.Local.Logger;
using SteamAccountManager.Domain.Steam.Storage;
using SteamAccountManager.Infrastructure.Steam.Local.Dto;

namespace SteamAccountManager.Infrastructure.Steam.Local.Storage
{
    public class SteamApiKeyStorage : ISteamApiKeyStorage
    {
        private const string FileName = "api_key.json";

        private ApiKeyStorageDto _apiKeyStorage;
        private readonly ILogger _logger;

        public SteamApiKeyStorage(ILogger logger)
        {
            _apiKeyStorage = new();
            _logger = logger;
            Load();
        }

        // TODO: throw custom exceptions

        private void Load()
        {
            try
            {
                using var streamReader = new StreamReader(FileName);
                var json = streamReader.ReadToEnd();
                _apiKeyStorage = JsonConvert.DeserializeObject<ApiKeyStorageDto>(json) ?? _apiKeyStorage;
            }
            catch (FileNotFoundException e)
            {
                _logger.LogException("File doesn't exist", e);
                Save();
            }
        }

        private void Save()
        {
            try
            {
                using (var streamWriter = new StreamWriter(FileName, append: false, Encoding.UTF8))
                {
                    var json = JsonConvert.SerializeObject(_apiKeyStorage, Formatting.Indented);
                    streamWriter.WriteLine(json);
                }
            }
            catch (Exception e)
            {
                _logger.LogException("Failed to save api key", e);
                throw;
            }
        }

        public string Get()
        {
            return _apiKeyStorage.Key;
        }

        public void Set(string value)
        {
            _apiKeyStorage.Key = value;
            Save();
        }
    }
}