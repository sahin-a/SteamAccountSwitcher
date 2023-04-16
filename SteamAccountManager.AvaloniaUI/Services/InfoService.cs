using System;
using System.Diagnostics;

namespace SteamAccountManager.AvaloniaUI.Services;

public class InfoService
{
    public void ShowRepository()
    {
        if (OperatingSystem.IsWindows())
            Process.Start("explorer", "https://github.com/sahin-a/SteamAccountManager/");
        else
            Process.Start("xdg-open", "https://github.com/sahin-a/SteamAccountManager/");
    }
}