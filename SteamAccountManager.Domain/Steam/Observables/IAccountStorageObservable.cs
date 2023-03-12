using SteamAccountManager.Domain.Observable;
using SteamAccountManager.Domain.Steam.Model;

namespace SteamAccountManager.Domain.Steam.Observables
{
    public interface IAccountStorageObservable : IObserveable<List<Account>?>
    {
    }
}
