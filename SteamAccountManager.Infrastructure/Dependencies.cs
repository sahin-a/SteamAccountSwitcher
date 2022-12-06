using Autofac;
using SteamAccountManager.Domain.Steam.Local.Repository;
using SteamAccountManager.Infrastructure.Steam.Local.Dao;
using SteamAccountManager.Infrastructure.Steam.Local.Repository;
using SteamAccountManager.Infrastructure.Steam.Local.Vdf;

namespace SteamAccountManager.Infrastructure
{
    public static class Dependencies
    {
        public static void RegisterDataModule(this ContainerBuilder builder)
        {
            builder.RegisterType<ISteamConfig>().As<SteamWinRegistryConfig>().SingleInstance();
            builder.RegisterType<ISteamLoginVdfParser>().As<SteamLoginVdfParser>().SingleInstance();
            builder.RegisterType<ISteamLoginVdfReader>().As<SteamLoginVdfReader>().SingleInstance();
            builder.RegisterType<ILoginUsersDao>().As<LoginUsersDao>().SingleInstance();
            builder.RegisterType<ISteamRepository>().As<SteamRepository>().SingleInstance();
        }
    }
}