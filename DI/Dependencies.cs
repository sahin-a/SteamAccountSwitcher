using Autofac;
using SteamAccountManager.Infrastructure;
using SteamAccountManager.Windows;
using System.Runtime.InteropServices;

namespace DI
{
    public static class Dependencies
    {
        public static void RegisterModules(this ContainerBuilder builder)
        {
            builder.RegisterInfrastructureModule();
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                builder.RegisterWindowsModule();
        }
    }
}