using ConfigureationApp2.MainAttribute;
using System.Reflection;

namespace ConfigureationApp2.Console
{
    public abstract class ConfigurationComponentBase
    {
        private readonly Dictionary<string, IConfigurationItemAttribute> _settings = new();

        protected ConfigurationComponentBase()
        {
            LoadAttributes();
        }

        private void LoadAttributes()
        {
            var properties = GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var property in properties)
            {
                var attribute = property.GetCustomAttributes(typeof(IConfigurationItemAttribute), false)
                                        .FirstOrDefault() as IConfigurationItemAttribute;
                if (attribute != null)
                {
                    _settings[property.Name] = attribute;
                }
            }
        }

        public void SaveSettings()
        {
            foreach (var setting in _settings)
            {
                var value = GetType().GetProperty(setting.Key).GetValue(this);
                setting.Value.SetValue(value);
            }
        }

        public void LoadSettings()
        {
            foreach (var setting in _settings)
            {
                var value = setting.Value.GetValue();
                var propertyType = GetType().GetProperty(setting.Key).PropertyType;

                if (value != null && propertyType.IsInstanceOfType(value))
                {
                    GetType().GetProperty(setting.Key).SetValue(this, value);
                }
                else if (propertyType == typeof(int) && int.TryParse(value?.ToString(), out var intValue))
                {
                    GetType().GetProperty(setting.Key).SetValue(this, intValue);
                }
                else if (propertyType == typeof(float) && float.TryParse(value?.ToString(), out var floatValue))
                {
                    GetType().GetProperty(setting.Key).SetValue(this, floatValue);
                }
                else if (propertyType == typeof(string))
                {
                    GetType().GetProperty(setting.Key).SetValue(this, value?.ToString());
                }
                else if (propertyType == typeof(TimeSpan) && TimeSpan.TryParse(value?.ToString(), out var timeSpanValue))
                {
                    GetType().GetProperty(setting.Key).SetValue(this, timeSpanValue);
                }
            }
        }
    }
}
