using DiscordRPC;

namespace SteamAccountManager.AvaloniaUI.Common.Discord;

public class DiscordRpcService
{
    public void Start()
    {
        var client = new DiscordRpcClient("1085336328020955196");
        client.Initialize();
        client.SetPresence(new RichPresence
        {
            Details = "github.com/sahin-a/SteamAccountSwitcher",
            State = "🧙‍♂️",
            Assets = new Assets
            {
                LargeImageKey = "sam_logo",
                LargeImageText = "github.com/sahin-a/SteamAccountSwitcher"
            }
        });
    }
}