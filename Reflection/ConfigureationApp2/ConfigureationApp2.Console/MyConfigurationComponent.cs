using ConfigureationApp2.ConfigurationManagerConfigurationProviderPlugin;
using ConfigureationApp2.FileConfigurationProviderPlugin;

namespace ConfigureationApp2.Console
{
    public class MyConfigurationComponent : ConfigurationComponentBase
    {
        //[ConfigurationManagerConfigurationItem(SettingName = "MyStringSetting")]
        [FileConfigurationItem(SettingName = "MyStringSetting")]
        public string MyStringSetting { get; set; }

        //[ConfigurationManagerConfigurationItem(SettingName = "MyFloatSetting")]
        [FileConfigurationItem(SettingName = "MyFloatSetting")]
        public float MyFloatSetting { get; set; }

        //[ConfigurationManagerConfigurationItem(SettingName = "MyIntSetting")]
        [FileConfigurationItem(SettingName = "MyIntSetting")]
        public int MyIntSetting { get; set; }
    }
}
