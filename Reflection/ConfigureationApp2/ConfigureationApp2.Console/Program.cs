using ConfigureationApp2.MainAttribute;
using System.Reflection;

class Program
{
    static void Main(string[] args)
    {
        string[] pluginPaths = new string[]
        {
            @"ConfigureationApp2.FileConfigurationProviderPlugin\bin\Debug\net8.0\ConfigureationApp2.FileConfigurationProviderPlugin.dll",
            @"ConfigureationApp2.ConfigurationManagerConfigurationProviderPlugin\bin\Debug\net8.0\ConfigureationApp2.ConfigurationManagerConfigurationProviderPlugin.dll"
        };

        IEnumerable<ConfigurationItemAttribute> commands = pluginPaths.SelectMany(pluginPath =>
        {
            Assembly pluginAssembly = AssemblyStaticCommand.LoadPlugin(pluginPath);
            return AssemblyStaticCommand.CreateCommands(pluginAssembly);
        }).ToList();


        Console.WriteLine("Commands: ");
        foreach (ConfigurationItemAttribute command in commands)
        {
            Console.WriteLine($"{command.SettingName}\t - {command}");
        }

    }
}
