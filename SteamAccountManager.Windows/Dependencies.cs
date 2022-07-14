using Autofac;
using SteamAccountManager.Application.Steam.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SteamAccountManager.Windows
{
    public static class Dependencies
    {
        public static void RegisterWindowsModule(this ContainerBuilder builder)
        {
                builder.RegisterType<WindowsLocalNotificationService>().As<ILocalNotificationService>().SingleInstance();
        }
    }
}
