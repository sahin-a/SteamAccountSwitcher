using Autofac;
using DI;

class Program
{
    public static IContainer Container { get; set; }

    public static void Main()
    {
        RegisterDependencies();
        new MainMenu();
    }

    private static void RegisterDependencies()
    {
        Autofac.ContainerBuilder builder = new();
        builder.RegisterModules();
        Container = builder.Build();
    }
}