namespace SteamAccountManager.AvaloniaUI.Common.Utils
{
    public static class TimeConverter
    {
        public static long ToDays(long minutes) => minutes / Time.DAY_IN_MINUTES;

        public static long ToHours(long minutes) => minutes / Time.HOUR_IN_MINUTES;
    }
}
