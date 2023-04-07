using System;
using System.IO;

namespace SteamAccountManager.Infrastructure.Steam.Local.Storage;

static class AppDataDirectory
{
    private static string createDirectory(string directory) =>
        Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "sahin-a",
            "Steam Account Switcher",
            directory
        );

    public static readonly string Configurations = createDirectory("Configurations");
    public static readonly string Storages = createDirectory("Storages");
    public static readonly string Logs = createDirectory("Logs");
}