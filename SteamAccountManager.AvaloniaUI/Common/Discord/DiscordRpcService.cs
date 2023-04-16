using System.Threading.Tasks;
using DiscordRPC;
using SteamAccountManager.Domain.Common.EventSystem;
using SteamAccountManager.Domain.Steam.Storage;

namespace SteamAccountManager.AvaloniaUI.Common.Discord;

public class DiscordRpcService
{
    private readonly DiscordRpcClient _client;
    private readonly EventBus _eventBus;
    private readonly IRichPresenceConfigStorage _richPresenceConfigStorage;

    public DiscordRpcService(EventBus eventBus, IRichPresenceConfigStorage richPresenceConfigStorage)
    {
        _eventBus = eventBus;
        _richPresenceConfigStorage = richPresenceConfigStorage;
        _client = new("1085336328020955196");

        _client.Initialize();
        SubscribeToConfigChanges();
    }

    private void SubscribeToConfigChanges()
    {
        _eventBus.Subscribe(subscriberKey: GetType().Name, Events.RICH_PRESENCE_CONFIG_UPDATED,
            _ => UpdateRichPresence()
        );
    }

    private async Task<bool> IsRichPresenceDisabled()
    {
        var config = await _richPresenceConfigStorage.Get();
        return config is not null && config.IsEnabled == false;
    }

    public async void UpdateRichPresence()
    {
        if (!_client.IsInitialized)
            return;

        if (await IsRichPresenceDisabled())
        {
            _client.ClearPresence();
            return;
        }

        _client.SetPresence(new RichPresence
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