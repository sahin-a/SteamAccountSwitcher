namespace SteamAccountManager.Domain.Steam.Configuration.Model;

public enum AccountDetailType
{
    LoginName,
    Username,
    Level,
    Avatar,
    BanStatus,
}

public class DetailSetting
{
    public AccountDetailType DetailType { get; set; }

    public bool IsEnabled { get; set; }

    public DetailSetting(AccountDetailType detailType, bool isEnabled)
    {
        DetailType = detailType;
        IsEnabled = isEnabled;
    }
}

public class PrivacyConfig
{
    public List<DetailSetting> DetailSettings { get; set; }

    public PrivacyConfig(List<DetailSetting> detailSettings)
    {
        DetailSettings = detailSettings;
    }
}