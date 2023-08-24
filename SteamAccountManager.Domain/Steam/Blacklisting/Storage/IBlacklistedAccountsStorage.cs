using SteamAccountManager.Domain.Common.Storage;
using SteamAccountManager.Domain.Steam.Blacklisting.Model;

namespace SteamAccountManager.Domain.Steam.Blacklisting.Storage;

public interface IBlacklistedAccountsStorage : IObjectStorage<AccountBlacklist>
{
}