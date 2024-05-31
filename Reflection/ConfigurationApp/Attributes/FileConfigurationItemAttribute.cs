using Newtonsoft.Json.Linq;

namespace ConfigurationApp.Attributes;

public class FileConfigurationItemAttribute : ConfigurationItemAttribute
{
    private static readonly string ConfigFilePath = "appsettings.json";

    public FileConfigurationItemAttribute(string settingName) : base(settingName) { }

    public override object GetValue()
    {
        if (!File.Exists(ConfigFilePath))
            return null;

        var json = File.ReadAllText(ConfigFilePath);
        var jObject = JObject.Parse(json);
        return jObject[SettingName]?.ToString();
    }

    public override void SetValue(object value)
    {
        JObject jObject;

        if (File.Exists(ConfigFilePath))
        {
            var json = File.ReadAllText(ConfigFilePath);
            jObject = JObject.Parse(json);
        }
        else
        {
            jObject = new JObject();
        }

        jObject[SettingName] = JToken.FromObject(value);
        File.WriteAllText(ConfigFilePath, jObject.ToString());
    }
}
