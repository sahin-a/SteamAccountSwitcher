
using System;
using Autofac;
using DI;
using SteamAccountManager.Domain.Steam.Service;

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

public class MainMenu
{
    private readonly ISteamService _steamService;
    
    public MainMenu()
    {
        try
        {
            _steamService = Program.Container.Resolve<ISteamService>();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        Console.WriteLine("Wait2ing..");
        ShowAccountSelection();
    }
    
    private async void ShowAccountSelection()
    {
        var accounts = await _steamService.GetAccounts();
        
        foreach (var account in accounts)
        {
            Console.WriteLine($"[Valid: {account.IsLoginTokenValid}] {account.AccountName}");
        }

        Console.WriteLine("Waiting..");
        Console.ReadLine();
    }
}