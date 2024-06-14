namespace ConfigureationApp2.MainAttribute
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public abstract class ConfigurationItemAttribute : Attribute
    {
        public string SettingName { get; }
        public string ProviderType { get; }

        protected ConfigurationItemAttribute(string settingName, string providerType)
        {
            SettingName = settingName;
            ProviderType = providerType;
        }

        public abstract object GetValue();
        public abstract void SetValue(object value);

        public static ConfigurationItemAttribute CreateInstance(string providerType, string settingName)
        {
            var type = Type.GetType(providerType);
            if (type == null) throw new Exception($"Provider type {providerType} not found.");
            return (ConfigurationItemAttribute)Activator.CreateInstance(type, settingName);
        }
    }
}
