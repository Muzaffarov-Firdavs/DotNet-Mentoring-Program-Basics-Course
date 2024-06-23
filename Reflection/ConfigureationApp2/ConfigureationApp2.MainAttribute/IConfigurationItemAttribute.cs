namespace ConfigureationApp2.MainAttribute
{
    public interface IConfigurationItemAttribute
    {
        string SettingName { get; }
        string ProviderType { get; }

        object GetValue();
        void SetValue(object value);

    }
}
