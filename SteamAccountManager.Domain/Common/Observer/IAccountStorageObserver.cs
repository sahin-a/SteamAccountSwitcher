using SteamAccountManager.Domain.Steam.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamAccountManager.Domain.Common.Observer
{
    public interface IAccountStorageObserver : IObserveable<List<Account>?>
    {
    }
}
