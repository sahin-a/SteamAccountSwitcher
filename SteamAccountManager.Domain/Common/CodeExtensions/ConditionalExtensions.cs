namespace SteamAccountManager.Domain.Common.CodeExtensions;

public static class ConditionalExtensions
{
    public static void IfTrue(this bool value, Action action)
    {
        if (value)
            action();
    }
}