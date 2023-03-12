using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SteamAccountManager.Domain.Common.Storage;
using SteamAccountManager.Domain.Steam.Local.Logger;

namespace SteamAccountManager.Infrastructure.Steam.Local.Storage;

public abstract class ObjectStorage<T> : IObjectStorage<T> where T : class
{
    private readonly string _fileName;
    private readonly ILogger _logger;

    private T? _value = null;

    public ObjectStorage(string fileName, ILogger logger)
    {
        _logger = logger;
        _fileName = $"{fileName}.json";
    }

    private string ReadJson()
    {
        string json = "";
        try
        {
            json = File.ReadAllText(_fileName);
        }
        catch
        {
        }

        return json;
    }

    public T? Get()
    {
        if (_value is not null)
            return _value;

        try
        {
            _value = JsonConvert.DeserializeObject<T>(ReadJson());
            return _value;
        }
        catch (Exception e)
        {
            _logger.LogException($"Failed to persist object of type {typeof(T).FullName}", e);
        }

        return null;
    }

    public void Set(T value)
    {
        _value = value;
        var json = JsonConvert.SerializeObject(value, Formatting.Indented, new StringEnumConverter());
        File.WriteAllText(_fileName, json);
    }
}