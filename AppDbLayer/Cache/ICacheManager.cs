using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLayer.Cache
{
    public interface ICacheManager
    {
        void Set<K, V>(K key, V value, int exp = 5);
        V Get<K, V>(K key);
        void Remove<K>(K key);
        V GetOrAdd<K, V>(K key, V value);
    }
}
