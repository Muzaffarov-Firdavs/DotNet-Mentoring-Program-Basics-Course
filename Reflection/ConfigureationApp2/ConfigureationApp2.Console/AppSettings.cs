using ConfigureationApp2.MainAttribute;

public class AppSettings : ConfigurationComponentBase
{
    [ConfigurationItemAttribute("MyAppSetting", "ConfigurationApp2.ConfigurationManagerConfigurationProviderPlugin.ConfigurationManagerConfigurationItemAttribute, ConfigurationManagerConfigurationProviderPlugin")]
    [ConfigurationItemAttribute("MyAppSetting", "ConfigurationApp2.FileConfigurationProviderPlugin.FileConfigurationItemAttribute, FileConfigurationProviderPlugin")]
    public string MyAppSetting { get; set; }
}
