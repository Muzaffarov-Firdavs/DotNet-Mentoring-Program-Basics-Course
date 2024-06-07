class Program
{
    static void Main(string[] args)
    {
        var s = TimeSpan.FromMinutes(23).ToString();

        var settings = new MySettings();

        settings.LoadSettings();

        Console.WriteLine($"Current MyIntSetting: {settings.MyIntSetting}");
        Console.WriteLine($"Current MyFloatSetting: {settings.MyFloatSetting}");
        Console.WriteLine($"Current MyStringSetting: {settings.MyStringSetting}");

        Console.Write("Enter new value for MyIntSetting: ");
        settings.MyIntSetting = int.Parse(Console.ReadLine());

        Console.Write("Enter new value for MyFloatSetting: ");
        settings.MyFloatSetting = float.Parse(Console.ReadLine());

        Console.Write("Enter new value for MyStringSetting: ");
        settings.MyStringSetting = Console.ReadLine();



        settings.SaveSettings();

        Console.WriteLine("Settings saved successfully.");
    }
}
