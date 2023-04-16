namespace SteamAccountManager.Domain.Common.CodeExtensions;

public static class ScopeExtensions
{
    public static R Let<T, R>(this T value, Func<T, R> action)
    {
        return action(value);
    }

    public static void With<T>(this T value, Action<T> action)
    {
        action(value);
    }
}