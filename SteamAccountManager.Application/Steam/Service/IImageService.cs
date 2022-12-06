namespace SteamAccountManager.Application.Steam.Service
{
    public interface IImageService
    {
        public Task<byte[]> GetImageAsync(string url);
    }
}
