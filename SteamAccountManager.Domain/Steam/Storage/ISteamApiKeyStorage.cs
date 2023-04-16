using SteamAccountManager.Domain.Common.Storage;

namespace SteamAccountManager.Domain.Steam.Storage;

public interface ISteamApiKeyStorage : IObjectStorage<string>
{
}