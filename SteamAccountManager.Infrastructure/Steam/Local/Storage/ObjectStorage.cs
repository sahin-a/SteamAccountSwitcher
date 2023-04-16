using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SteamAccountManager.Domain.Common.CodeExtensions;
using SteamAccountManager.Domain.Common.Storage;
using SteamAccountManager.Domain.Steam.Local.Logger;
using SteamAccountManager.Infrastructure.Steam.Local.DataSource;

namespace SteamAccountManager.Infrastructure.Steam.Local.Storage;

public class ObjectStorage<T> : IObjectStorage<T> where T : class
{
    private readonly IKeyValueDataSource _dataSource;
    private readonly string _name;
    private readonly ILogger _logger;

    private T? _value = null;

    public ObjectStorage(string name, ILogger logger, IKeyValueDataSource dataSource)
    {
        _logger = logger;
        _name = name;
        _dataSource = dataSource;
    }

    protected virtual T? GetDefaultValue() => null;

    /// <summary>
    /// Retrieves the value as non-nullable
    /// </summary>
    /// <param name="defaultValue"></param>
    /// <returns>value as non-nullable</returns>
    /// <exception cref="NullReferenceException">Will be thrown when there is no value or default value present</exception>
    public async Task<T> Get(T defaultValue) =>
        await Get() ?? throw new NullReferenceException(message: "No value could be retrieved!");

    public async Task<T?> Get()
    {
        if (_value is not null)
            return _value;

        var json = await _dataSource.Load(_name);

        if (json is null)
            return GetDefaultValue()?.Let(value => value);

        try
        {
            _value = JsonConvert.DeserializeObject<T>(json);

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

    public async Task Set(T value)
    {
        _value = value;
        var json = JsonConvert.SerializeObject(value, Formatting.Indented, new StringEnumConverter());
        await _dataSource.Store(_name, json);
    }
}