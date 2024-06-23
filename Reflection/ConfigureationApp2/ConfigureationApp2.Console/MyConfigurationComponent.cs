using ConfigureationApp2.ConfigurationManagerConfigurationProviderPlugin;
using ConfigureationApp2.FileConfigurationProviderPlugin;

namespace ConfigureationApp2.Console
{
    public class MyConfigurationComponent : ConfigurationComponentBase
    {
        [FileConfigurationItem(SettingName = "MyString")]
        [ConfigurationManagerConfigurationItem(SettingName = "MyString")]
        public string MyStringSetting { get; set; }

        [FileConfigurationItem(SettingName = "MyFloat")]
        [ConfigurationManagerConfigurationItem(SettingName = "MyFloat")]
        public float MyFloatSetting { get; set; }

        [FileConfigurationItem(SettingName = "MyInt")]
        [ConfigurationManagerConfigurationItem(SettingName = "MyInt")]
        public int MyIntSetting { get; set; }
    }
}
