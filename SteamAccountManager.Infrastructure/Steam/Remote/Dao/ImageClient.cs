using SteamAccountManager.Infrastructure.Steam.Exceptions;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace SteamAccountManager.Infrastructure.Steam.Local.Dao
{
    internal class ImageClient
    {
        private readonly HttpClient _httpClient;

        public ImageClient()
        {
            _httpClient = new HttpClient();
        }

        public async Task<byte[]> DownloadImage(string url)
        {
            try
            {
                var response = await _httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode || response.Content.Headers.ContentLength <= 0)
                {
                    throw new RequestNotSuccessfulException(response.ReasonPhrase ?? "Request failed");
                }

                return await response.Content.ReadAsByteArrayAsync();
            }
            catch (Exception e)
            {
                throw new ImageDownloadFailedException("Image download failed", e);
            }
        }
    }
}
