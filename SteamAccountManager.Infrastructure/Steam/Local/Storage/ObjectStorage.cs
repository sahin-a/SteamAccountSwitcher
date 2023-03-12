using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SteamAccountManager.Domain.Common.Storage;

namespace SteamAccountManager.Infrastructure.Steam.Local.Storage;

public abstract class ObjectStorage<T> : IObjectStorage<T> where T : class
{
    private string _fileName;

    public ObjectStorage(string fileName)
    {
        _fileName = $"{fileName}.json";
    }

    private string ReadJson()
    {
        string json = "";
        try
        {
            json = File.ReadAllText(_fileName);
        }
        catch (Exception)
        {
        }

        return json;
    }

    public T? Get()
    {
        try
        {
            return JsonConvert.DeserializeObject<T>(ReadJson());
        }
        catch (Exception)
        {
        }

        return null;
    }

    public void Set(T value)
    {
        var json = JsonConvert.SerializeObject(value, Formatting.Indented, new StringEnumConverter());
        File.WriteAllText(_fileName, json);
    }
}