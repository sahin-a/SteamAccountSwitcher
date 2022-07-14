using Autofac;
using SteamAccountManager.Infrastructure;
using SteamAccountManager.Windows;

namespace DI
{
    public static class Dependencies
    {
        public static void RegisterModules(this ContainerBuilder builder)
        {
            builder.RegisterInfrastructureModule();
            builder.RegisterWindowsModule();
        }
    }
}