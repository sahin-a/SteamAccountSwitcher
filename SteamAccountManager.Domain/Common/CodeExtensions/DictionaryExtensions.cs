using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamAccountManager.Domain.Common.CodeExtensions
{
    public static class DictionaryExtensions
    {
        public static V? GetOrNull<K, V>(this Dictionary<K, V> map, K key) where K : class where V : class
            => map.TryGetValue(key, out V? value) ? value : null;
    }
}
