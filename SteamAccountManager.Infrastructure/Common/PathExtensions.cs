using System.IO;

namespace SteamAccountManager.Infrastructure.Common;

public static class PathExtensions
{
    public static string ToSafeFileName(this string value) 
        => string.Join("", value.Split(Path.GetInvalidFileNameChars()));
}