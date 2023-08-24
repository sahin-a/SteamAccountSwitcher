using System;
using System.Diagnostics;

namespace SteamAccountManager.AvaloniaUI.Services;

public class InfoService
{
    private const string RetrieveApiKey = "https://steamcommunity.com/dev/apikey";
    private const string GithubRepository = "https://github.com/sahin-a/SteamAccountSwitcher/";

    public void ShowRepository() => ShowWebPage(GithubRepository);

    public void ShowRetrieveApiKey() => ShowWebPage(RetrieveApiKey);

    private void ShowWebPage(string url)
    {
        var targetExecutable = OperatingSystem.IsWindows() ? "explorer" : "xdg-open";
        Process.Start(targetExecutable, url);
    }
}