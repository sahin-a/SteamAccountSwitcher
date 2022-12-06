using SteamAccountManager.Domain.Common.Observer;
using SteamAccountManager.Domain.Steam.Model;
using System;
using System.Collections.Generic;

namespace SteamAccountManager.Infrastructure.Steam.Local.Observer
{
    internal abstract class AccountStorageObserver : IAccountStorageObserver
    {
        private List<Action<List<Account>?>> _observers = new();

        public void Notify(List<Account>? value)
        {
            _observers.ForEach(x => x.Invoke(value));
        }

        public IDisposable Subscribe(Action<List<Account>?> observeable)
        {
            _observers.Add(observeable);
            return new Unsubscriber<List<Account>?>(_observers, observeable);
        }
    }
}
