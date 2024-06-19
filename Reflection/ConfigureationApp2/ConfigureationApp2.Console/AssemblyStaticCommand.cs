using ConfigureationApp2.Console;
using ConfigureationApp2.MainAttribute;
using System.Reflection;
using System.Windows.Input;

public class AssemblyStaticCommand
{
    public static Assembly LoadPlugin(string relativePath)
    {
        // Navigate up to the solution root
        string root = Path.GetFullPath(Path.Combine(
            Path.GetDirectoryName(
                Path.GetDirectoryName(
                    Path.GetDirectoryName(
                        Path.GetDirectoryName(
                            Path.GetDirectoryName(typeof(Program).Assembly.Location)))))));

        string pluginLocation = Path.GetFullPath(Path.Combine(root, relativePath.Replace('\\', Path.DirectorySeparatorChar)));
        Console.WriteLine($"Loading commands from: {pluginLocation}");
        PluginLoadContext loadContext = new PluginLoadContext(pluginLocation);
        return loadContext.LoadFromAssemblyName(AssemblyName.GetAssemblyName(pluginLocation));
    }

    public static IEnumerable<ConfigurationItemAttribute> CreateCommands(Assembly assembly)
    {
        int count = 0;

        foreach (Type type in assembly.GetTypes())
        {
            if (typeof(ConfigurationItemAttribute).IsAssignableFrom(type))
            {
                ConfigurationItemAttribute result = Activator.CreateInstance(type) as ConfigurationItemAttribute;
                if (result != null)
                {
                    count++;
                    yield return result;
                }
            }
        }

        if (count == 0)
        {
            string availableTypes = string.Join(",", assembly.GetTypes().Select(t => t.FullName));
            throw new ApplicationException(
                $"Can't find any type which implements ICommand in {assembly} from {assembly.Location}.\n" +
                $"Available types: {availableTypes}");
        }
    }
}