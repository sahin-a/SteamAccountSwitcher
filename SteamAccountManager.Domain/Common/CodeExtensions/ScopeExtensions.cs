namespace SteamAccountManager.Domain.Common.CodeExtensions;

public static class ScopeExtensions
{
    public static R Let<T, R>(this T value, Func<T, R> action)
    {
        return action(value);
    }
}