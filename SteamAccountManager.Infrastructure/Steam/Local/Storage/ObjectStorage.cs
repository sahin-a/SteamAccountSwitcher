using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SteamAccountManager.Domain.Common.CodeExtensions;
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

    protected virtual T? GetDefaultValue() => null;

    /// <summary>
    /// Retrieves the value as non-nullable
    /// </summary>
    /// <param name="defaultValue"></param>
    /// <returns>value as non-nullable</returns>
    /// <exception cref="NullReferenceException">Will be thrown when there is no value or default value present</exception>
    public T Get(T defaultValue) => Get() ?? throw new NullReferenceException(message: "No value could be retrieved!");

    public T? Get()
    {
        if (_value is not null)
            return _value;

        try
        {
            _value = JsonConvert.DeserializeObject<T>(ReadJson());

            if (_value is null)
                throw new NullReferenceException($"Failed to deserialize object! Content: {_value}");

            return _value;
        }
        catch (Exception e)
        {
            _logger.LogException($"Failed to retrieve persisted object of type {typeof(T).FullName}", e);
        }

        return GetDefaultValue()?.Let(value => value);
    }

    public void Set(T value)
    {
        _value = value;
        var json = JsonConvert.SerializeObject(value, Formatting.Indented, new StringEnumConverter());
        File.WriteAllText(_fileName, json);
    }
}