using Autofac;
using DI;
using SteamAccountManager.WinUI.SAM.Steam.ViewModel;

namespace SteamAccountManager.WinUI
{
    internal static class Dependencies
    {
        public static IContainer Container { get; private set; }

        public static void RegisterDependencies()
        {
            ContainerBuilder builder = new();
            builder.RegisterModules();
            builder.RegisterViewModels();

            Container = builder.Build();
        }

        public static void RegisterViewModels(this ContainerBuilder builder)
        {
            builder.RegisterType<AccountSwitcherViewModel>();
        }
    }
}
