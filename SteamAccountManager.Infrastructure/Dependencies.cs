using System.Runtime.InteropServices;
using Autofac;
using SteamAccountManager.Domain.Steam.Local.Logger;
using SteamAccountManager.Domain.Steam.Local.Repository;
using SteamAccountManager.Domain.Steam.Observables;
using SteamAccountManager.Domain.Steam.Service;
using SteamAccountManager.Domain.Steam.Storage;
using SteamAccountManager.Domain.Steam.UseCase;
using SteamAccountManager.Infrastructure.Common;
using SteamAccountManager.Infrastructure.Common.Logging;
using SteamAccountManager.Infrastructure.Steam.Local.Dao;
using SteamAccountManager.Infrastructure.Steam.Local.DataSource;
using SteamAccountManager.Infrastructure.Steam.Local.FileWatcher;
using SteamAccountManager.Infrastructure.Steam.Local.Repository;
using SteamAccountManager.Infrastructure.Steam.Local.Storage;
using SteamAccountManager.Infrastructure.Steam.Local.Vdf;
using SteamAccountManager.Infrastructure.Steam.Remote.Dao;
using SteamAccountManager.Infrastructure.Steam.Service;

namespace SteamAccountManager.Infrastructure
{
    public static class Dependencies
    {
        public static void RegisterInfrastructureModule(this ContainerBuilder builder)
        {
#if DEBUG
            builder.RegisterType<DebugLogger>().As<ILogger>().SingleInstance();
#else
            builder.RegisterType<FileLogger>().As<ILogger>().SingleInstance();
#endif
            builder.RegisterStorages();

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
            builder.RegisterType<SwitchAccountUseCase>().As<ISwitchAccountUseCase>().SingleInstance();
            builder.RegisterType<GetAccountsWithDetailsUseCase>().As<IGetAccountsWithDetailsUseCase>().SingleInstance();
            builder.RegisterType<SteamProcessService>().As<ISteamProcessService>().SingleInstance();
            builder.RegisterType<SteamWebClient>().As<ISteamWebClient>().SingleInstance();
            builder.RegisterType<SteamUserProvider>().As<ISteamUserProvider>().SingleInstance();
            builder.RegisterType<SteamProfileService>().As<ISteamProfileService>().SingleInstance();
            builder.RegisterType<ImageClient>().SingleInstance();
            builder.RegisterType<ImageService>().As<IImageService>().SingleInstance();
            builder.RegisterType<SteamPlayerServiceProvider>().As<ISteamPlayerServiceProvider>().SingleInstance();
            builder.RegisterType<SteamPlayerService>().As<ISteamPlayerService>().SingleInstance();
            builder.RegisterType<LoginVdfFileWatcher>().As<IAccountStorageObservable>().SingleInstance();
            builder.RegisterType<AvatarStorage>().SingleInstance();
            builder.RegisterType<UserAvatarStorage>().SingleInstance();
            builder.RegisterType<AvatarService>().As<IAvatarService>().SingleInstance();
        }

        private static void RegisterStorages(this ContainerBuilder builder)
        {
            builder.RegisterType<SteamApiKeyStorage>().As<ISteamApiKeyStorage>().SingleInstance();
            builder.RegisterType<PrivacyConfigStorage>().As<IPrivacyConfigStorage>().SingleInstance();
        }
    }
}