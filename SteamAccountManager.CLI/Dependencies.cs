using Autofac;
using DI;
using SteamAccountManager.CLI.Steam;

namespace SteamAccountManager.CLI
{
    internal static class Dependencies
    {
        public static IContainer? Container { get; set; }
        public static void RegisterDependencies()
        {
            ContainerBuilder builder = new();
            builder.RegisterModules();
            builder.RegisterType<ListCommand>().SingleInstance();
            builder.RegisterType<SwitchCommand>().SingleInstance();
            builder.RegisterType<SteamAccountSwitcherCommandHandler>().SingleInstance();

            Container = builder.Build();
        }
    }
}
