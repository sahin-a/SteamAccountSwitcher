namespace SteamAccountManager.Domain.Steam.Service
{
    public interface IImageService
    {
        public Task<byte[]> GetImageAsync(string url);
    }
}
