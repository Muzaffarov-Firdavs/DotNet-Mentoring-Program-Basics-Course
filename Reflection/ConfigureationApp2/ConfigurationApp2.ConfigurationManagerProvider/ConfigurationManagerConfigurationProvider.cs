namespace ConfigurationApp2.ConfigurationManagerProvider
{
    public class ConfigurationManagerConfigurationProvider
    {
        public string GetValue(string settingName)
        {
            return ConfigurationManager.AppSettings[settingName];
        }

        public void SetValue(string settingName, string value)
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings.Remove(settingName);
            config.AppSettings.Settings.Add(settingName, value);
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }
    }
}
