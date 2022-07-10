namespace SteamAccountManager.Application.Steam.Service;

public class AvatarResponse
{
    public AvatarResponse(Uri path, byte[] payload)
    {
        Path = path;
        Payload = payload;
    }

    public Uri Path { get; }
    public byte[] Payload { get; }
}

public interface IAvatarService
{
    public Task<AvatarResponse?> GetAvatarAsync(string url);
}