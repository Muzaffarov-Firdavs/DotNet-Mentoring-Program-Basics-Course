using ConfigureationApp2.MainAttribute;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace ConfigureationApp2.FileConfigurationProviderPlugin
{
    public class FileConfigurationItemAttribute : Attribute, IConfigurationItemAttribute
    {
        private const string ConfigFilePath = @"../../../app.config";

        public string SettingName { get; set; }
        public string ProviderType { get => "File"; }

        public object GetValue()
        {
            if (!File.Exists(ConfigFilePath))
                throw new Exception("app.config does not exist or is in the wrong path given.");

            var xml = XDocument.Load(ConfigFilePath);
            var element = xml.Descendants("appSettings")
                             .Descendants("add")
                             .FirstOrDefault(x => x.Attribute("key")?.Value == SettingName);

            if (element == null)
                throw new Exception($"Setting '{SettingName}' not found in app.config.");

            return element.Attribute("value")?.Value;
        }

        public void SetValue(object value)
        {
            XDocument xml;
            if (File.Exists(ConfigFilePath))
            {
                xml = XDocument.Load(ConfigFilePath);
            }
            else
            {
                xml = new XDocument(new XElement("configuration", new XElement("appSettings")));
            }

            var element = xml.Descendants("appSettings")
                             .Descendants("add")
                             .FirstOrDefault(x => x.Attribute("key")?.Value == SettingName);

            if (element != null)
            {
                element.SetAttributeValue("value", value);
            }
            else
            {
                xml.Element("configuration")?.Element("appSettings")?.Add(new XElement("add", new XAttribute("key", SettingName), new XAttribute("value", value)));
            }

            xml.Save(ConfigFilePath);
        }
    }
}
