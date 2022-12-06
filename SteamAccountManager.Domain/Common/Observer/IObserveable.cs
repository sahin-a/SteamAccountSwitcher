using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamAccountManager.Domain.Common.Observer
{
    public interface IObserveable<T>
    {
        public IDisposable Subscribe(Action<T?> observeable);
        public void Notify(T? value);
    }
}
