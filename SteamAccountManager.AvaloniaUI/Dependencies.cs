using Autofac;
using DI;
using ReactiveUI;
using SteamAccountManager.AvaloniaUI.Mappers;
using SteamAccountManager.AvaloniaUI.Notifications;
using SteamAccountManager.AvaloniaUI.Services;
using SteamAccountManager.AvaloniaUI.ViewModels;
using SteamAccountManager.Domain.Steam.Service;

namespace SteamAccountManager.AvaloniaUI
{
    internal static class Dependencies
    {
        public static IContainer? Container { get; set; }

        public static void RegisterDependencies()
        {
            ContainerBuilder builder = new();
            builder.RegisterModules();
            builder.RegisterAvaloniaModule();
            builder.RegisterViewModels();

            Container = builder.Build();
        }

        public static void RegisterAvaloniaModule(this ContainerBuilder builder)
        {
            builder.RegisterType<AvatarService>().SingleInstance();
            builder.RegisterType<AccountMapper>().SingleInstance();
#if WINDOWS10_0_17763_0_OR_GREATER
            builder.RegisterType<WindowsLocalNotificationService>().As<ILocalNotificationService>().SingleInstance();
#else
            builder.RegisterType<LegacyWindowsLocalNotificationService>().As<ILocalNotificationService>().SingleInstance();
#endif
        }

        public static void RegisterViewModels(this ContainerBuilder builder)
        {
            builder.RegisterViewModel<AccountSwitcherViewModel>();
            builder.RegisterViewModel<SettingsViewModel>();
        }

        private static void RegisterViewModel<ViewModel>(this ContainerBuilder builder)
            where ViewModel : RoutableViewModel
        {
            builder.RegisterType<ViewModel>()
                .WithParameter(new TypedParameter(typeof(IScreen), "screen"));
        }
    }
}