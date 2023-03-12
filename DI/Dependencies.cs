using Autofac;
using SteamAccountManager.Infrastructure;

namespace DI
{
    public static class Dependencies
    {
        public static ContainerBuilder RegisterModules(this ContainerBuilder builder)
        {
            builder.RegisterInfrastructureModule();
            return builder;
        }
    }
}