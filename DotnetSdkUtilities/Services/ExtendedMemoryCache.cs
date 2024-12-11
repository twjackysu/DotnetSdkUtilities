using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetSdkUtilities.Services
{
    public class ExtendedMemoryCache : MemoryCache, IExtendedMemoryCache
    {
        private readonly HashSet<string> _cacheKeys;

        public ExtendedMemoryCache(MemoryCacheOptions options) : base(options)
        {
            _cacheKeys = new HashSet<string>();
        }
        public HashSet<string> Keys { 
            get {
                var keys = new HashSet<string>(_cacheKeys);
                for(int i = 0; i < keys.Count; i++)
                {
                    if (!base.TryGetValue(keys.ElementAt(i), out _))
                    {
                        _cacheKeys.Remove(keys.ElementAt(i));
                    }
                }
                return _cacheKeys;
            }
        }

        public TItem Set<TItem>(object key, TItem value)
        {
            using ICacheEntry entry = base.CreateEntry(key);
            entry.Value = value;

            _cacheKeys.Add(key.ToString());
            return value;
        }

        public TItem Set<TItem>(object key, TItem value, DateTimeOffset absoluteExpiration)
        {
            using ICacheEntry entry = base.CreateEntry(key);
            entry.AbsoluteExpiration = absoluteExpiration;
            entry.Value = value;

            _cacheKeys.Add(key.ToString());
            return value;
        }

        public TItem Set<TItem>(object key, TItem value, TimeSpan absoluteExpirationRelativeToNow)
        {
            using ICacheEntry entry = base.CreateEntry(key);
            entry.AbsoluteExpirationRelativeToNow = absoluteExpirationRelativeToNow;
            entry.Value = value;

            _cacheKeys.Add(key.ToString());
            return value;
        }

        public TItem Set<TItem>(object key, TItem value, IChangeToken expirationToken)
        {
            using ICacheEntry entry = base.CreateEntry(key);
            entry.AddExpirationToken(expirationToken);
            entry.Value = value;

            _cacheKeys.Add(key.ToString());
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
            _cacheKeys.Add(key.ToString());
            return value;
        }
        public TItem GetOrCreate<TItem>(object key, Func<ICacheEntry, TItem> factory)
        {
            if (!base.TryGetValue(key, out object result))
            {
                using ICacheEntry entry = base.CreateEntry(key);

                result = factory(entry);
                entry.Value = result;
                _cacheKeys.Add(key.ToString());
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
                _cacheKeys.Add(key.ToString());
            }

            return (TItem)result;
        }
        public new void Remove(object key)
        {
            base.Remove(key);
            _cacheKeys.Remove(key.ToString());
        }

        public void ClearCacheByContains(string value)
        {
            var keysToRemove = _cacheKeys.Where(key => key.Contains(value)).ToList();
            foreach (var key in keysToRemove)
            {
                base.Remove(key);
                _cacheKeys.Remove(key);
            }
        }
    }
}
