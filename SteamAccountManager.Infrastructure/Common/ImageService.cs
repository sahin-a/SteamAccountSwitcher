using System;
using System.Threading.Tasks;
using SteamAccountManager.Domain.Steam.Local.Logger;
using SteamAccountManager.Domain.Steam.Service;
using SteamAccountManager.Infrastructure.Steam.Exceptions;
using SteamAccountManager.Infrastructure.Steam.Local.Dao;
using SteamAccountManager.Infrastructure.Steam.Remote.Dao;

namespace SteamAccountManager.Infrastructure.Common
{
    internal class ImageService : IImageService
    {
        private readonly ImageClient _imageClient;
        private readonly ILogger _logger;

        public ImageService(ImageClient imageClient, ILogger logger)
        {
            _imageClient = imageClient;
            _logger = logger;
        }

        public async Task<byte[]> GetImageAsync(string url)
        {
            try
            {
                _logger.LogDebug($"Starting image download for {url}");
                return await _imageClient.DownloadImage(url);
            }
            catch (ImageDownloadFailedException e)
            {
                _logger.LogException("Failed to download the image", e);
            }

            return Array.Empty<byte>();
        }
    }
}
