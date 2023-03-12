using SteamAccountManager.Domain.Steam.Configuration.Model;
using SteamAccountManager.Domain.Steam.Storage;

namespace SteamAccountManager.Infrastructure.Steam.Local.Storage;

public class PrivacyConfigStorage : ObjectStorage<PrivacyConfig>, IPrivacyConfigStorage
{
    public PrivacyConfigStorage(string fileName = "account_details_privacy") : base(fileName)
    {
    }
}