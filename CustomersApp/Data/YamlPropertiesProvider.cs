using System.Collections.Generic;
using System.IO;
using YamlDotNet.Serialization;

namespace CustomersApp;

public class YamlPropertiesProvider
{
    private const string FileName = "config.yaml";

    private Dictionary<string, List<Dictionary<string, string>>> _properties; 
    
    public YamlPropertiesProvider()
    {
        var deserializer = new DeserializerBuilder().Build();
        var yaml = File.ReadAllText($"./{FileName}");
        _properties = deserializer.Deserialize<Dictionary<string, List<Dictionary<string, string>>>>(yaml);
    }

    public string GetDbServer()
    {
        return _properties["database"][0]["server"];
    }

    public string GetServerPort()
    {
        return _properties["database"][0]["port"];
    }

    public string GetDbName()
    {
        return _properties["database"][0]["database"];
    }

    public string GetDbUserId()
    {
        return _properties["database"][0]["user_id"];
    }

    public string GetDbPassword()
    {
        return _properties["database"][0]["password"];
    }

    public string GetAppPassword()
    {
        return _properties["log_in_screen"][0]["password"];
    }
    
}