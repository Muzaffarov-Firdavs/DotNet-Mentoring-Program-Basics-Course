using ConfigureationApp2.MainAttribute;
using System;
using System.Configuration;

namespace ConfigureationApp2.ConfigurationManagerConfigurationProviderPlugin
{
    public class ConfigurationManagerConfigurationItemAttribute : Attribute, IConfigurationItemAttribute
    {
        public string SettingName { get; set; }
        public string ProviderType { get => "ConfigurationManager"; }

        public object GetValue()
        {
            return ConfigurationManager.AppSettings[SettingName];
        }

        public void SetValue(object value)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings.Remove(SettingName);
            config.AppSettings.Settings.Add(SettingName, value.ToString());
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }
    }
}
