using SteamAccountManager.Application.Common.Observable;
using SteamAccountManager.Domain.Steam.Model;

namespace SteamAccountManager.Application.Steam.Observables
{
    public interface IAccountStorageObservable : IObserveable<List<Account>?>
    {
    }
}
