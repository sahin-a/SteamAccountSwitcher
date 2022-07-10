using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using SteamAccountManager.Application.Steam.Local.Logger;
using SteamAccountManager.Application.Steam.Service;
using SteamAccountManager.Infrastructure.Steam.Local.Storage;

namespace SteamAccountManager.Infrastructure.Steam.Service;

public class AvatarService : IAvatarService
{
    private const string STEAM_AVATAR_URL_SCHEME = @"https://avatars.akamai.steamstatic.com/";

    private readonly AvatarStorage _avatarStorage;
    private readonly ILogger _logger;
    private readonly IImageService _imageService;

    public AvatarService(ILogger logger, AvatarStorage avatarStorage, IImageService imageService)
    {
        _avatarStorage = avatarStorage;
        _logger = logger;
        _imageService = imageService;
    }

    private string ExtractFileName(string url)
        => url.Replace(STEAM_AVATAR_URL_SCHEME, "");

    private async Task<AvatarResponse?> DownloadAvatarAsync(string url, string fileName)
    {
        var imagePayload = await _imageService.GetImageAsync(url);

        if (imagePayload.LongLength <= 0L)
            return null;

        _avatarStorage.Store(fileName, imagePayload);
        var uri = _avatarStorage.GetUri(fileName);

        return new AvatarResponse(uri!, imagePayload);
    }

    public async Task<AvatarResponse?> GetAvatarAsync(string url)
    {
        var id = ExtractFileName(url);

        switch (Regex.IsMatch(STEAM_AVATAR_URL_SCHEME, $"^{STEAM_AVATAR_URL_SCHEME}"))
        {
            case false:
                _logger.LogWarning($"Url didn't match pattern: {url}");
                return null;
        }

        var cachedAvatar = _avatarStorage.GetUri(id);
        switch (cachedAvatar is not null)
        {
            case true:
                _logger.LogDebug($"{id} is available in cache, falling back to cached version");
                var payload = await _avatarStorage.GetBytesAsync(id);
                return new AvatarResponse(cachedAvatar, payload!);
            default:
                _logger.LogDebug($"Avatar not available in storage yet, starting download of {url}");
                return await DownloadAvatarAsync(url, id);
        }
    }
}