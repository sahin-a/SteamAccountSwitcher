using Avalonia;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using System;
using System.IO;
using System.Threading.Tasks;
using SteamAccountManager.Domain.Steam.Service;

namespace SteamAccountManager.AvaloniaUI.Services
{
    public class AvatarService
    {
        private IAssetLoader _assetLoader;
        private readonly IAvatarService _avatarService;

        private readonly Uri FALLBACK_AVATAR_URI =
            new Uri("avares://SteamAccountManager.AvaloniaUI/Assets/avatar_placeholder.jpg", UriKind.Absolute);

        public AvatarService(IAvatarService avatarService)
        {
            _avatarService = avatarService;
            _assetLoader = AvaloniaLocator.Current.GetService<IAssetLoader>()
                           ?? throw new Exception("Failed to resolve AssetLoader");
        }

        private Bitmap CreateBitmap(byte[] imagePayload)
        {
            Stream stream = new MemoryStream(imagePayload);
            return new Bitmap(stream);
        }

        private Bitmap GetFallbackAvatar() => new(_assetLoader.Open(FALLBACK_AVATAR_URI));

        public async Task<Tuple<Uri, IBitmap>> GetAvatarAsync(string steamId, string url)
        {
            var image = await _avatarService.GetAvatarAsync(steamId, url);

            if (image is null)
                return new Tuple<Uri, IBitmap>(FALLBACK_AVATAR_URI, GetFallbackAvatar());

            return new Tuple<Uri, IBitmap>(image.Path, CreateBitmap(image.Payload));
        }
    }
}
