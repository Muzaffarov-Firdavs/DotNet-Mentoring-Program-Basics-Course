using System.Reflection;

namespace ConfigurationApp2.Console.Attributes
{
    public class ConfigurationManagerConfigurationItemAttribute : ConfigurationItemAttribute
    {
        public ConfigurationManagerConfigurationItemAttribute(string settingName) : base(settingName) { }

        public override object GetValue()
        {
            var provider = LoadProvider("ConfigurationManagerConfigurationProvider.dll", "ConfigurationManagerConfigurationProvider.ConfigurationManagerConfigurationProvider");
            return provider.GetType().GetMethod("GetValue").Invoke(provider, new object[] { SettingName });
        }

        public override void SetValue(object value)
        {
            var provider = LoadProvider("ConfigurationManagerConfigurationProvider.dll", "ConfigurationManagerConfigurationProvider.ConfigurationManagerConfigurationProvider");
            provider.GetType().GetMethod("SetValue").Invoke(provider, new object[] { SettingName, value.ToString() });
        }

        private object LoadProvider(string assemblyName, string typeName)
        {
            var assemblyPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, assemblyName);
            var assembly = Assembly.LoadFrom(assemblyPath);
            return assembly.CreateInstance(typeName);
        }
    }
}
