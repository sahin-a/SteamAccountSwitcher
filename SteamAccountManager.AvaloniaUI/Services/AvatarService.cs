// using Avalonia;
// using Avalonia.Media.Imaging;
// using Avalonia.Platform;
// using SteamAccountManager.Application.Steam.Service;
// using System;
// using System.Diagnostics;
// using System.IO;
// using System.Threading.Tasks;
//
// namespace SteamAccountManager.AvaloniaUI.Services
// {
//     // TODO: REEEFAAAAAAAAAAACTOR no way I'll merge this in this state to master
//
//     // TODO: REEEFAAAAAAAAAAACTOR no way I'll merge this in this state to master
//     internal class AvatarService
//     {
//         private readonly IAssetLoader _assetLoader;
//
//         public AvatarService(IImageService imageService)
//         {
//             _assetLoader = AvaloniaLocator.Current.GetService<IAssetLoader>()
//                            ?? throw new Exception("Failed to resolve AssetLoader");
//         }
//
//         private Bitmap CreateBitmap(byte[] payload)
//             {
//                 Stream stream = new MemoryStream(payload);
//                 return new Bitmap(stream);
//             }
//
//         public async Task<Tuple<Uri?, Bitmap?>> GetAvatarAsync(string url)
//         {
//             var fileName = ExtractFileName(url);
//             var cachedImage = await GetFromCache(fileName);
//             var imagePayload = cachedImage?.Image ?? await _imageService.GetImageAsync(url);
//
//             if (string.IsNullOrEmpty(url) || imagePayload.Length == 0)
//             {
//                 var placeholderImage = ;
//                 return Tuple.Create(placeholderImage, new Bitmap(_assetLoader.Open(placeholderImage)));
//             }
//
//
//
//             return Tuple.Create((await _avatarStorage.Retrieve(fileName))?.Path, image);
//         }
//     }
// }
