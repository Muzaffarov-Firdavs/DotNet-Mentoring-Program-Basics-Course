using ConfigureationApp2.MainAttribute;
using Newtonsoft.Json.Linq;

namespace ConfigureationApp2.FileConfigurationProviderPlugin
{
    public class FileConfigurationItemAttribute : ConfigurationItemAttribute
    {
        private static readonly string ConfigFilePath = @"../../../appsettings.json";

        public FileConfigurationItemAttribute(string settingName) : base(settingName) { }

        public override object GetValue()
        {
            if (!File.Exists(ConfigFilePath))
                throw new Exception("appsettings.json is not exist or in the wrong path given.");

            var json = File.ReadAllText(ConfigFilePath);
            var jObject = JObject.Parse(json);
            return jObject[SettingName];
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
}
