using ConfigureationApp2.Console;
using System.Reflection;

class Program
{
    static void Main(string[] args)
    {
        string[] pluginPaths =
        {
            @"ConfigureationApp2.FileConfigurationProviderPlugin\bin\Debug\net8.0\ConfigureationApp2.FileConfigurationProviderPlugin.dll",
            @"ConfigureationApp2.ConfigurationManagerConfigurationProviderPlugin\bin\Debug\net8.0\ConfigureationApp2.ConfigurationManagerConfigurationProviderPlugin.dll"
        };

        try
        {
            // Load plugins
            foreach (var pluginPath in pluginPaths)
            {
                LoadPlugin(pluginPath);
            }

            // Create and configure component
            var component = new MyConfigurationComponent();
            component.LoadSettings();

            // Display settings
            Console.WriteLine($"MyStringSetting: {component.MyStringSetting}");
            Console.WriteLine($"MyFloatSetting: {component.MyFloatSetting}");
            Console.WriteLine($"MyIntSetting: {component.MyIntSetting}");

            // Modify and save settings
            Console.Write("Enter new value for MyIntSetting: ");
            component.MyIntSetting = int.Parse(Console.ReadLine());
            Console.Write("Enter new value for MyFloatSetting: ");
            component.MyFloatSetting = float.Parse(Console.ReadLine());
            Console.Write("Enter new value for MyStringSetting: ");
            component.MyStringSetting = Console.ReadLine();

            component.SaveSettings();
            Console.WriteLine("Settings updated and saved.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }

    static void LoadPlugin(string relativePath)
    {
        string root = Path.GetFullPath(Path.Combine(
            Path.GetDirectoryName(typeof(Program).Assembly.Location), @"..\..\..\..\"));

        string pluginLocation = Path.GetFullPath(Path.Combine(root, relativePath.Replace('\\', Path.DirectorySeparatorChar)));
        Console.WriteLine($"Loading plugin from: {pluginLocation}");
        PluginLoadContext loadContext = new PluginLoadContext(pluginLocation);
        loadContext.LoadFromAssemblyName(AssemblyName.GetAssemblyName(pluginLocation));
    }
}
