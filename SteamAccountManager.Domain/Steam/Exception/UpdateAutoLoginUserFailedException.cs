using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamAccountManager.Domain.Steam.Exception
{
    public class UpdateAutoLoginUserFailedException : System.Exception
    {
        public UpdateAutoLoginUserFailedException() { }
        public UpdateAutoLoginUserFailedException(string message) : base(message) { }
    }
}
