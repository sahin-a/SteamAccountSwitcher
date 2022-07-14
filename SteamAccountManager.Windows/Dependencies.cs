using Autofac;
using SteamAccountManager.Application.Steam.Service;
using SteamAccountManager.Windows.Notifications;

namespace SteamAccountManager.Windows
{
    public static class Dependencies
    {
        public static void RegisterWindowsModule(this ContainerBuilder builder)
        {
#if WINDOWS10_0_17763_0_OR_GREATER
            builder.RegisterType<WindowsLocalNotificationService>().As<ILocalNotificationService>().SingleInstance();
#else
            builder.RegisterType<LegacyWindowsLocalNotificationService>().As<ILocalNotificationService>().SingleInstance();
#endif
        }
    }
}
