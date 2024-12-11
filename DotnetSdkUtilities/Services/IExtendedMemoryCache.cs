using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotnetSdkUtilities.Services
{
    public interface IExtendedMemoryCache : IMemoryCache
    {
        HashSet<string> Keys { get; }
        TItem Set<TItem>(object key, TItem value);
        TItem Set<TItem>(object key, TItem value, DateTimeOffset absoluteExpiration);
        TItem Set<TItem>(object key, TItem value, TimeSpan absoluteExpirationRelativeToNow);
        TItem Set<TItem>(object key, TItem value, IChangeToken expirationToken);
        TItem Set<TItem>(object key, TItem value, MemoryCacheEntryOptions options);
        TItem GetOrCreate<TItem>(object key, Func<ICacheEntry, TItem> factory);
        Task<TItem> GetOrCreateAsync<TItem>(object key, Func<ICacheEntry, Task<TItem>> factory);
        void ClearCacheByContains(string value);
    }
}
