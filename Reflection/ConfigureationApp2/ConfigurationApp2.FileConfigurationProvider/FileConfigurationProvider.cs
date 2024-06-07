using Newtonsoft.Json.Linq;

namespace ConfigurationApp2.FileConfigurationProvider
{
    public class FileConfigurationProvider
    {
        private static readonly string ConfigFilePath = "appsettings.json";

        public string GetValue(string settingName)
        {
            if (!File.Exists(ConfigFilePath))
                return null;

            var json = File.ReadAllText(ConfigFilePath);
            var jObject = JObject.Parse(json);
            return jObject[settingName]?.ToString();
        }

        public void SetValue(string settingName, string value)
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

            jObject[settingName] = JToken.FromObject(value);
            File.WriteAllText(ConfigFilePath, jObject.ToString());
        }
    }
}
