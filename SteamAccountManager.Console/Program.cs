using Autofac;
using DI;
using SteamAccountManager.Console.Menu;

class Program
{
    public static IContainer Container { get; private set; }

    public static void Main()
    {
        RegisterDependencies();
        new MainMenu().Show();
    }

    private static void RegisterDependencies()
    {
        ContainerBuilder builder = new();
        builder.RegisterModules();
        Container = builder.Build();
    }
}