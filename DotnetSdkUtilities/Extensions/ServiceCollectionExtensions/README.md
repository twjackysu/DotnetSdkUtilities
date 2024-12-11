[Back](https://github.com/twjackysu/DotnetSdkUtilities/blob/master/README.md)

# ServiceCollectionExtensions

## ExtendedMemoryCache

ExtendedMemoryCache is a custom implementation of IMemoryCache that tracks cache keys and provides additional functionality, such as retrieving all cache keys and clearing cache entries by a pattern. This is particularly useful in scenarios where you need access to all cache keys (e.g., pagination or clearing part of a user's repository in specific pages).

#### Features

- Retrieve all cache keys using the `Keys` property.

- Clear cache entries by a specific key pattern using `ClearCacheByContains`.

- Fully compatible with .NET `MemoryCache` features.

- Designed for dependency injection (DI).

### How to Use

1. Register ExtendedMemoryCache

   Use the AddExtendedMemoryCache extension method to register the cache in your application's IServiceCollection.
    ```csharp
    using Microsoft.Extensions.DependencyInjection;

    var services = new ServiceCollection();
    services.AddExtendedMemoryCache(); // or in your web app builder.Services.AddExtendedMemoryCache();
    // or
    var serviceProvider = services.BuildServiceProvider();
    var cache = serviceProvider.GetRequiredService<IExtendedMemoryCache>();
    ```
2. Use Dependency Injection to Access IExtendedMemoryCache

    After registering the service, you can inject IExtendedMemoryCache into your classes and use it.
    ```csharp
    public class MyService
    {
        private readonly IExtendedMemoryCache _cache;

        public MyService(IExtendedMemoryCache cache)
        {
            _cache = cache;
        }

        public void DoSomething()
        {
            // Add an entry to the cache
            _cache.Set("key1", "value1");

            // Retrieve all keys
            var keys = _cache.Keys;
        }
    }
    ```
3. Clear Cache by Keys Contains

    Use the `ClearCacheByContains` method to remove cache entries that match a specific pattern.

    Example

    Suppose your cache keys are structured with user identifiers (e.g., user:123:page:1). If you want to clear all cache entries for user 123:
    ```csharp
    // Clear all entries for user 123
    cache.ClearCacheByContains("user:123");
    ```

## Notes

This implementation keeps track of cache keys using an internal HashSet. Make sure the cache keys are unique and represented as strings.

When using Keys, expired entries will automatically be removed from the internal collection.

Use cautiously in high-scale environments where frequent access to all cache keys might impact performance.