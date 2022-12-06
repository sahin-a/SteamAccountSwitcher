using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamAccountManager.Application.Steam.Service
{
    public interface IImageService
    {
        public Task<byte[]> GetImageAsync(string url);
    }
}
