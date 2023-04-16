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
            Details = "One step ahead 🧑‍💻",
            Assets = new Assets
            {
                LargeImageKey = "sam_logo",
                LargeImageText = "You're missing out bro, go get it! 😂"
            },
            Buttons = new[]
            {
                new Button
                {
                    Label = "GitHub 💻",
                    Url = "https://github.com/sahin-a/SteamAccountSwitcher",
                },
                new Button
                {
                    Label = "Download 🌍",
                    Url = "https://github.com/sahin-a/SteamAccountSwitcher/releases",
                },
            },
        });
    }
}