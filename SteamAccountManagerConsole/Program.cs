
using System;
using Autofac;
using DI;
using SteamAccountManager.Domain.Steam.Local.POCO;
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
    private List<SteamLoginUser> SteamAccounts { get; set; } = new List<SteamLoginUser>();

    public MainMenu()
    {
        _steamService = Program.Container.Resolve<ISteamService>();
        SteamAccounts = _steamService.GetAccounts().Result;

        ShowAccountSelection();
    }

    private void ShowAccountSelection()
    {
        Console.Clear();

        for (int i = 0; i < SteamAccounts.Count; i++)
        {
            var account = SteamAccounts[i];
            Console.WriteLine($"{i}. [Valid: {account.IsLoginTokenValid}] {account.AccountName}");
        }

        Console.WriteLine("Enter Number to log in account, Habibi!!");

        string? accountSelection = Console.ReadLine();
        
        if (Int32.TryParse(accountSelection, out int accountIndex))
        {
            _steamService.LogInAccount(SteamAccounts[accountIndex].AccountName);
        }
        
        ShowAccountSelection();
    }
}