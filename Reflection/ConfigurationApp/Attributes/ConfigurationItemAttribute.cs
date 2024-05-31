namespace ConfigurationApp.Attributes;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
public abstract class ConfigurationItemAttribute : Attribute
{
    public string SettingName { get; }

    public ConfigurationItemAttribute(string settingName)
    {
        SettingName = settingName;
    }

    public abstract object GetValue();
    public abstract void SetValue(object value);
}
