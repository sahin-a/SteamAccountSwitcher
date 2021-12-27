using Autofac;
using SteamAccountManager.Application.Steam.Local.Logger;
using SteamAccountManager.Application.Steam.Local.Repository;
using SteamAccountManager.Application.Steam.Service;
using SteamAccountManager.Infrastructure.Steam.Local.Dao;
using SteamAccountManager.Infrastructure.Steam.Local.DataSource;
using SteamAccountManager.Infrastructure.Steam.Local.Logger;
using SteamAccountManager.Infrastructure.Steam.Local.Repository;
using SteamAccountManager.Infrastructure.Steam.Local.Vdf;
using SteamAccountManager.Infrastructure.Steam.Remote.Dao;
using SteamAccountManager.Infrastructure.Steam.Service;

namespace SteamAccountManager.Infrastructure
{
    public static class Dependencies
    {
        public static void RegisterInfrastructureModule(this ContainerBuilder builder)
        {
            builder.RegisterType<DebugLogger>().As<ILogger>().SingleInstance();
            builder.RegisterType<SteamWinRegistryConfig>().As<ISteamConfig>().SingleInstance();
            builder.RegisterType<SteamLoginVdfParser>().As<ISteamLoginVdfParser>().SingleInstance();
            builder.RegisterType<SteamLoginVdfReader>().As<ISteamLoginVdfReader>().SingleInstance();
            builder.RegisterType<LoginUsersDao>().As<ILoginUsersDao>().SingleInstance();
            builder.RegisterType<LocalSteamDataSource>().As<ILocalSteamDataSource>().SingleInstance();
            builder.RegisterType<SteamRepository>().As<ISteamRepository>().SingleInstance();
            builder.RegisterType<SteamService>().As<ISteamService>().SingleInstance();
            builder.RegisterType<SteamProcessService>().As<ISteamProcessService>().SingleInstance();
            builder.RegisterType<SteamWebClient>().As<ISteamWebClient>().SingleInstance();
            builder.RegisterType<SteamUserProvider>().As<ISteamUserProvider>().SingleInstance();
            builder.RegisterType<SteamProfileService>().As<ISteamProfileService>().SingleInstance();
            builder.RegisterType<ImageClient>().SingleInstance();
            builder.RegisterType<ImageService>().As<IImageService>().SingleInstance();
            builder.RegisterType<SteamPlayerServiceProvider>().As<ISteamPlayerServiceProvider>().SingleInstance();
        }
    }
}