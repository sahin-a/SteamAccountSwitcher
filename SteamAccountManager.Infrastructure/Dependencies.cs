using Autofac;
using SteamAccountManager.Application.Steam.Local.Logger;
using SteamAccountManager.Application.Steam.Local.Repository;
using SteamAccountManager.Application.Steam.Service;
using SteamAccountManager.Application.Steam.UseCase;
using SteamAccountManager.Domain.Steam.Observables;
using SteamAccountManager.Infrastructure.Steam.Local.Dao;
using SteamAccountManager.Infrastructure.Steam.Local.DataSource;
using SteamAccountManager.Infrastructure.Steam.Local.FileWatcher;
using SteamAccountManager.Infrastructure.Steam.Local.Logger;
using SteamAccountManager.Infrastructure.Steam.Local.Repository;
using SteamAccountManager.Infrastructure.Steam.Local.Storage;
using SteamAccountManager.Infrastructure.Steam.Local.Vdf;
using SteamAccountManager.Infrastructure.Steam.Remote.Dao;
using SteamAccountManager.Infrastructure.Steam.Service;
using System.Runtime.InteropServices;

namespace SteamAccountManager.Infrastructure
{
    public static class Dependencies
    {
        public static void RegisterInfrastructureModule(this ContainerBuilder builder)
        {
            builder.RegisterType<DebugLogger>().As<ILogger>().SingleInstance();
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                builder.RegisterType<SteamLinuxRegistryConfig>().As<ISteamConfig>().SingleInstance();
            }
            else
            {
                builder.RegisterType<SteamWinRegistryConfig>().As<ISteamConfig>().SingleInstance();
            }
            builder.RegisterType<SteamLoginVdfParser>().As<ISteamLoginVdfParser>().SingleInstance();
            builder.RegisterType<SteamLoginVdfReader>().As<ISteamLoginVdfReader>().SingleInstance();
            builder.RegisterType<LoginUsersDao>().As<ILoginUsersDao>().SingleInstance();
            builder.RegisterType<LocalSteamDataSource>().As<ILocalSteamDataSource>().SingleInstance();
            builder.RegisterType<SteamRepository>().As<ISteamRepository>().SingleInstance();
            builder.RegisterType<SwitchAccountUseCase>().SingleInstance();
            builder.RegisterType<GetAccountsWithDetailsUseCase>().As<IGetAccountsWithDetailsUseCase>().SingleInstance();
            builder.RegisterType<SteamProcessService>().As<ISteamProcessService>().SingleInstance();
            builder.RegisterType<SteamWebClient>().As<ISteamWebClient>().SingleInstance();
            builder.RegisterType<SteamUserProvider>().As<ISteamUserProvider>().SingleInstance();
            builder.RegisterType<SteamProfileService>().As<ISteamProfileService>().SingleInstance();
            builder.RegisterType<ImageClient>().SingleInstance();
            builder.RegisterType<ImageService>().As<IImageService>().SingleInstance();
            builder.RegisterType<SteamPlayerServiceProvider>().As<ISteamPlayerServiceProvider>().SingleInstance();
            builder.RegisterType<SteamPlayerService>().As<ISteamPlayerService>().SingleInstance();
            builder.RegisterType<SteamApiKeyStorage>().SingleInstance();
            builder.RegisterType<LoginVdfFileWatcher>().As<IAccountStorageObservable>().SingleInstance();
            //builder.RegisterType<LocalNotificationService>().As<ILocalNotificationService>().SingleInstance();
        }
    }
}