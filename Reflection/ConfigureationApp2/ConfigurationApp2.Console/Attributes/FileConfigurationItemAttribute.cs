using System.Reflection;

namespace ConfigurationApp2.Console.Attributes
{
    public class FileConfigurationItemAttribute : ConfigurationItemAttribute
    {
        public FileConfigurationItemAttribute(string settingName) : base(settingName) { }

        public override object GetValue()
        {
            var provider = LoadProvider("FileConfigurationProvider.dll", "FileConfigurationProvider.FileConfigurationProvider");
            return provider.GetType().GetMethod("GetValue").Invoke(provider, new object[] { SettingName });
        }

        public override void SetValue(object value)
        {
            var provider = LoadProvider("FileConfigurationProvider.dll", "FileConfigurationProvider.FileConfigurationProvider");
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
