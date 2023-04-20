namespace SteamAccountManager.Domain.Steam.Blacklisting.Model;

public class AccountBlacklist
{
    public HashSet<string> BlacklistedIds { get; set; } = new();
}