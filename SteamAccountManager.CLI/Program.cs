using Autofac;
using CLAP;
using SteamAccountManager.CLI.Steam;

namespace SteamAccountManager.CLI;

public class Program
{
    public static int Main(string[] args)
    {
        Dependencies.RegisterDependencies();
        var steamAccountSwitcher = Dependencies.Container!.Resolve<SteamAccountSwitcherCommandHandler>();
        return Parser.RunConsole(args, steamAccountSwitcher);
    }
}