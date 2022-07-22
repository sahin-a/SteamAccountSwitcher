using Autofac;
using SteamAccountManager.Infrastructure;

namespace DI
{
    public static class Dependencies
    {
        public static void RegisterModules(this ContainerBuilder builder)
        {
            builder.RegisterInfrastructureModule();
        }
    }
}