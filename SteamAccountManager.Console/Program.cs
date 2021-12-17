using Autofac;
using DI;
using SteamAccountManager.Console.Menu;

class Program
{
#pragma warning disable CS8618 // Ein Non-Nullable-Feld muss beim Beenden des Konstruktors einen Wert ungleich NULL enthalten. Erwägen Sie die Deklaration als Nullable.
    public static IContainer Container { get; private set; }
#pragma warning restore CS8618 // Ein Non-Nullable-Feld muss beim Beenden des Konstruktors einen Wert ungleich NULL enthalten. Erwägen Sie die Deklaration als Nullable.

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