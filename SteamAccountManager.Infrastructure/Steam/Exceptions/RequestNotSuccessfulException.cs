using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamAccountManager.Infrastructure.Steam.Exceptions
{
    public class RequestNotSuccessfulException : Exception
    {
        public RequestNotSuccessfulException() { }
        public RequestNotSuccessfulException(string message) : base(message) { }
        public RequestNotSuccessfulException(string message, Exception inner) : base(message, inner) { }
    }
}
