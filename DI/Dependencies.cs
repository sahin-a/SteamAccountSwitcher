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
#if WINDOWS10_0_17763_0_OR_GREATER
            builder.RegisterWindowsModule();
#endif
        }
    }
}