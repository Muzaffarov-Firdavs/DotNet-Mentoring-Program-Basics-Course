using System.Reflection;

class Program
{
    static void Main(string[] args)
    {
        LoadPlugins();

        var settings = new AppSettings();
        settings.LoadSettings();

        Console.WriteLine($"Current Setting: {settings.MyAppSetting}");

        settings.MyAppSetting = "hfjhgigjgkjg";
        settings.SaveSettings();

        Console.WriteLine($"Updated Setting: {settings.MyAppSetting}");
    }

    static void LoadPlugins()
    {
        var pluginFolder = Path.Combine(AppContext.BaseDirectory);
        foreach (var dll in Directory.GetFiles(pluginFolder, "*.dll"))
        {
            Assembly.LoadFrom(dll);
        }
    }
}