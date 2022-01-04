using Avalonia;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using SteamAccountManager.Application.Steam.Service;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SteamAccountManager.AvaloniaUI.Services
{
    internal class AvatarService
    {
        private IImageService _imageService;
        private IAssetLoader _assetLoader;

        public AvatarService(IImageService imageService)
        {
            _imageService = imageService;
            _assetLoader = AvaloniaLocator.Current.GetService<IAssetLoader>();
        }

        public async Task<Bitmap> GetAvatarAsync(string url)
        {
            var imageBytes = await _imageService.GetImageAsync(url);

            if (string.IsNullOrEmpty(url) || imageBytes.Length == 0)
            {
                return new Bitmap(_assetLoader.Open(new Uri("avares://SteamAccountManager.AvaloniaUI/Assets/avatar_placeholder.jpg", UriKind.Absolute)));
            }

            Stream stream = new MemoryStream(imageBytes);
            var image = new Bitmap(stream);

            return image;
        }
    }
}
