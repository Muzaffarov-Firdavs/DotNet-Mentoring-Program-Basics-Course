using ConfigurationApp.Attributes;

namespace ConfigurationApp;

public class MySettings : ConfigurationComponentBase
{
    [FileConfigurationItem("MyIntSetting")]
    [ConfigurationManagerConfigurationItem("MyIntSetting")]
    public int MyIntSetting { get; set; }

    [FileConfigurationItem("MyStringSetting")]
    [ConfigurationManagerConfigurationItem("MyStringSetting")]
    public string MyStringSetting { get; set; }
}

