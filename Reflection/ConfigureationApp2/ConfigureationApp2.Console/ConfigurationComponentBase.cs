namespace ConfigureationApp2.MainAttribute
{
    public abstract class ConfigurationComponentBase
    {
        public void LoadSettings()
        {
            var properties = this.GetType().GetProperties();
            foreach (var property in properties)
            {
                var attributes = property.GetCustomAttributes(typeof(ConfigurationItemAttribute), false)
                                         .Cast<ConfigurationItemAttribute>();
                foreach (var attribute in attributes)
                {
                    var provider = ConfigurationItemAttribute.CreateInstance(attribute.ProviderType, attribute.SettingName);
                    var value = provider.GetValue();
                    property.SetValue(this, Convert.ChangeType(value, property.PropertyType));
                }
            }
        }

        public void SaveSettings()
        {
            var properties = this.GetType().GetProperties();
            foreach (var property in properties)
            {
                var attributes = property.GetCustomAttributes(typeof(ConfigurationItemAttribute), false)
                                         .Cast<ConfigurationItemAttribute>();
                foreach (var attribute in attributes)
                {
                    var provider = ConfigurationItemAttribute.CreateInstance(attribute.ProviderType, attribute.SettingName);
                    var value = property.GetValue(this);
                    provider.SetValue(value);
                }
            }
        }
    }
}
