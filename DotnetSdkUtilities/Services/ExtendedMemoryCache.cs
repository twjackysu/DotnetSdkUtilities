using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetSdkUtilities.Services
{
    public class ExtendedMemoryCache : MemoryCache, IExtendedMemoryCache
    {
        // MemoryCache itself is thread-safe and is normally registered as a singleton,
        // so the tracked key set has to tolerate concurrent access as well.
        // The value is unused, only the key set matters.
        private readonly ConcurrentDictionary<string, byte> _cacheKeys;

        public ExtendedMemoryCache(MemoryCacheOptions options) : base(options)
        {
            _cacheKeys = new ConcurrentDictionary<string, byte>();
        }
        public HashSet<string> Keys {
            get {
                var aliveKeys = new HashSet<string>();
                // ConcurrentDictionary.Keys returns a snapshot, so removing while iterating is safe.
                foreach (var key in _cacheKeys.Keys)
                {
                    if (base.TryGetValue(key, out _))
                    {
                        aliveKeys.Add(key);
                    }
                    else
                    {
                        _cacheKeys.TryRemove(key, out _);
                    }
                }
                return aliveKeys;
            }
        }

        public TItem Set<TItem>(object key, TItem value)
        {
            using ICacheEntry entry = base.CreateEntry(key);
            entry.Value = value;

            _cacheKeys[key.ToString()] = 0;
            return value;
        }

        public TItem Set<TItem>(object key, TItem value, DateTimeOffset absoluteExpiration)
        {
            using ICacheEntry entry = base.CreateEntry(key);
            entry.AbsoluteExpiration = absoluteExpiration;
            entry.Value = value;

            _cacheKeys[key.ToString()] = 0;
            return value;
        }

        public TItem Set<TItem>(object key, TItem value, TimeSpan absoluteExpirationRelativeToNow)
        {
            using ICacheEntry entry = base.CreateEntry(key);
            entry.AbsoluteExpirationRelativeToNow = absoluteExpirationRelativeToNow;
            entry.Value = value;

            _cacheKeys[key.ToString()] = 0;
            return value;
        }

        public TItem Set<TItem>(object key, TItem value, IChangeToken expirationToken)
        {
            using ICacheEntry entry = base.CreateEntry(key);
            entry.AddExpirationToken(expirationToken);
            entry.Value = value;

            _cacheKeys[key.ToString()] = 0;
            return value;
        }

        public TItem Set<TItem>(object key, TItem value, MemoryCacheEntryOptions options)
        {
            using ICacheEntry entry = base.CreateEntry(key);
            if (options != null)
            {
                entry.SetOptions(options);
            }

            entry.Value = value;
            _cacheKeys[key.ToString()] = 0;
            return value;
        }
        public TItem GetOrCreate<TItem>(object key, Func<ICacheEntry, TItem> factory)
        {
            if (!base.TryGetValue(key, out object result))
            {
                using ICacheEntry entry = base.CreateEntry(key);

                result = factory(entry);
                entry.Value = result;
                _cacheKeys[key.ToString()] = 0;
            }

            return (TItem)result;
        }

        public async Task<TItem> GetOrCreateAsync<TItem>(object key, Func<ICacheEntry, Task<TItem>> factory)
        {
            if (!base.TryGetValue(key, out object result))
            {
                using ICacheEntry entry = base.CreateEntry(key);

                result = await factory(entry).ConfigureAwait(false);
                entry.Value = result;
                _cacheKeys[key.ToString()] = 0;
            }

            return (TItem)result;
        }
        public new void Remove(object key)
        {
            base.Remove(key);
            _cacheKeys.TryRemove(key.ToString(), out _);
        }

        public void ClearCacheByContains(string value)
        {
            var keysToRemove = _cacheKeys.Keys.Where(key => key.Contains(value)).ToList();
            foreach (var key in keysToRemove)
            {
                base.Remove(key);
                _cacheKeys.TryRemove(key, out _);
            }
        }
    }
}
