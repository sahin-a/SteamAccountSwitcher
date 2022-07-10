using Avalonia;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using SteamAccountManager.Application.Steam.Service;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace SteamAccountManager.AvaloniaUI.Services
{
    // TODO: REEEFAAAAAAAAAAACTOR no way I'll merge this in this state to master
    class AvatarStorage
    {
        private readonly string CACHE_DIR = Path.Combine("Cache", "Avatar");

        public class Cache
        {
            public Uri Path { get; set; }
            public byte[] Image { get; set; }
        }

        public async Task<Cache?> Retrieve(string id)
        {
            var path = Path.Combine(CACHE_DIR, id);
            byte[]? bytes = null;
            try
            {
                bytes = await File.ReadAllBytesAsync(path);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }

            return bytes is null ? null : new Cache
            {
                Path = new Uri(Path.GetFullPath(path), UriKind.Absolute),
                Image = bytes
            };
        }

        public void Store(string id, byte[] bytes)
        {
            Directory.CreateDirectory(CACHE_DIR);
            var path = Path.Combine(CACHE_DIR, id);
            File.WriteAllBytesAsync(path, bytes);
        }
    }

    // TODO: REEEFAAAAAAAAAAACTOR no way I'll merge this in this state to master
    internal class AvatarService
    {
        private IImageService _imageService;
        private IAssetLoader _assetLoader;
        private AvatarStorage _avatarStorage;

        public AvatarService(IImageService imageService)
        {
            _imageService = imageService;
            _assetLoader = AvaloniaLocator.Current.GetService<IAssetLoader>()
                ?? throw new Exception("Failed to resolve AssetLoader");
            _avatarStorage = new AvatarStorage();
        }

        private string ExtractFileName(string url)
        {
            return url.Replace(@"https://avatars.akamai.steamstatic.com/", "");
        }

        public async Task<Tuple<Uri?, Bitmap?>> GetAvatarAsync(string url)
        {
            var fileName = ExtractFileName(url);
            var storedImage = await _avatarStorage.Retrieve(fileName);
            var imagePayload = storedImage?.Image ?? await _imageService.GetImageAsync(url);

            if (storedImage is null)
            {
                _avatarStorage.Store(fileName, imagePayload);
            }

            if (string.IsNullOrEmpty(url) || imagePayload.Length == 0)
            {
                var placeholderImage = new Uri("avares://SteamAccountManager.AvaloniaUI/Assets/avatar_placeholder.jpg", UriKind.Absolute);
                return Tuple.Create(placeholderImage, new Bitmap(_assetLoader.Open(placeholderImage)));
            }

            Stream stream = new MemoryStream(imagePayload);
            var image = new Bitmap(stream);

            return Tuple.Create((await _avatarStorage.Retrieve(fileName))?.Path, image);
        }
    }
}
