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
    private const string STEAM_AVATAR_DOMAIN = @"https://avatars.akamai.steamstatic.com/";
    
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
        => url.Replace(STEAM_AVATAR_DOMAIN, "");

    private async Task<AvatarResponse?> DownloadAvatarAsync(string url, string fileName)
    {
        var imagePayload = await _imageService.GetImageAsync(url);

        if (imagePayload.LongLength <= 0L)
            return null;
        
        _avatarStorage.Store(fileName, imagePayload);
        var uri = _avatarStorage.GetUri(fileName);
        
        return uri is null ? null : new AvatarResponse(uri, imagePayload);
    }

    private async Task<AvatarResponse> GetFallbackAvatar()
    {
        var uri = new Uri("", UriKind.Absolute);
        var payload = await File.ReadAllBytesAsync(uri.AbsolutePath);
        return new AvatarResponse(uri, payload);
    }
    
    public async Task<AvatarResponse> GetAvatarAsync(string url)
    {
        var id = ExtractFileName(url);
        
        if (!Regex.IsMatch(STEAM_AVATAR_DOMAIN, $"^{STEAM_AVATAR_DOMAIN}"))
        {
            _logger.LogWarning($"Url didn't match pattern: {url}");
            return await GetFallbackAvatar();
        }

        var cachedAvatar = _avatarStorage.GetUri(id);
        var isCachedAvatarAvailable = cachedAvatar is not null;

        if (isCachedAvatarAvailable)
        {
            var payload = await _avatarStorage.GetBytesAsync(id);
            return payload is null ? null : new AvatarResponse(cachedAvatar, payload);
        }
        
        return await DownloadAvatarAsync(url, id) ?? await GetFallbackAvatar();
    }
}