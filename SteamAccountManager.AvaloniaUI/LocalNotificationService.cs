﻿using Microsoft.Toolkit.Uwp.Notifications;
using SteamAccountManager.Application.Steam.Service;
using System;

namespace SteamAccountManager.AvaloniaUI
{
    public class LocalNotificationService : ILocalNotificationService
    {
        public void Send(Notification notification)
        {
            var builder = new ToastContentBuilder();

            if (notification.Title is not null)
                builder.AddText(notification.Title);

            if (notification.Message is not null)
                builder.AddText(notification.Message);

            if (notification.Logo is not null)
                builder.AddAppLogoOverride(notification.Logo, ToastGenericAppLogoCrop.Circle);

            builder.Show();
        }
    }
}