using SteamAccountManager.Infrastructure.Steam.Local.Storage;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using SteamAccountManager.Domain.Steam.Local.Logger;
using SteamAccountManager.Domain.Steam.Service;

namespace SteamAccountManager.Infrastructure.Steam.Service;

public class AvatarService : IAvatarService
{
    private const string STEAM_AVATAR_URL_SCHEME = @"https://avatars.akamai.steamstatic.com/";

    private readonly AvatarStorage _avatarStorage;
    private readonly ILogger _logger;
    private readonly IImageService _imageService;
    private readonly UserAvatarStorage _userAvatarMapStorage;

    public AvatarService(ILogger logger, AvatarStorage avatarStorage, IImageService imageService, UserAvatarStorage userAvatarMapStorage)
    {
        _avatarStorage = avatarStorage;
        _logger = logger;
        _imageService = imageService;
        _userAvatarMapStorage = userAvatarMapStorage;
    }

    private string ExtractFileName(string url)
        => url.Replace(STEAM_AVATAR_URL_SCHEME, "");

    private async Task<AvatarResponse?> DownloadAvatarAsync(string steamId, string url, string fileName)
    {
        var imagePayload = await _imageService.GetImageAsync(url);

        if (imagePayload.LongLength <= 0L)
            return null;

        _avatarStorage.Store(fileName, imagePayload);
        var uri = _avatarStorage.GetUri(fileName);

        return new AvatarResponse(uri!, imagePayload);
    }

    public async Task<AvatarResponse?> GetAvatarAsync(string steamId, string url)
    {
        var avatarId = ExtractFileName(url);

        switch (Regex.IsMatch(STEAM_AVATAR_URL_SCHEME, $"^{STEAM_AVATAR_URL_SCHEME}"))
        {
            case false:
                _logger.LogWarning($"Url didn't match pattern: {url}");
                return null;
        }

        if (string.IsNullOrEmpty(avatarId))
            avatarId = _userAvatarMapStorage.Get(steamId, string.Empty);

        var cachedAvatar = _avatarStorage.GetUri(avatarId);
        AvatarResponse response;
        switch (cachedAvatar is not null)
        {
            case true:
                _logger.LogDebug($"{avatarId} is available in cache, falling back to cached version");
                var payload = await _avatarStorage.GetBytesAsync(avatarId);
                response = new AvatarResponse(cachedAvatar, payload!);
                break;
            default:
                _logger.LogDebug($"Avatar not available in storage yet, starting download of {url}");
                response = await DownloadAvatarAsync(steamId, url, avatarId);
                break;
        }
        _userAvatarMapStorage.Store(steamId, avatarId);

        return response;
    }
}