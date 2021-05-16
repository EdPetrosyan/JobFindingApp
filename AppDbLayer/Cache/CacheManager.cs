using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLayer.Cache
{
    public class CacheManager : ICacheManager
    {
        private readonly IMemoryCache _cache;

        public CacheManager(IMemoryCache cache)
        {
            _cache = cache;
        }

        public void Set<K,V>(K key, V value, int exp = 5)
        {
            _cache.Set<V>(key, value, new MemoryCacheEntryOptions() { AbsoluteExpiration = DateTime.Now.AddMinutes(exp) });
        }

        public void Remove<K>(K key)
        {
            _cache.Remove(key);
        }

        public V Get<K,V>(K key)
        {
            V value;
            if(!_cache.TryGetValue(key,out value))
            {
                value = default;
            }
            return value;
        }

        public V GetOrAdd<K,V>(K key, V value)
        {
            V result;
            if (!_cache.TryGetValue(key, out result))
            {
                Set(key, value);
                result = value;
            }
            return result;
        }
    }
}
