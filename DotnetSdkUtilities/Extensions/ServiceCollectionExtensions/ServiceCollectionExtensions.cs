using DotnetSdkUtilities.Services;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace DotnetSdkUtilities.Extensions.ServiceCollectionExtensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddExtendedMemoryCache(this IServiceCollection services)
        {
            services.AddSingleton<IExtendedMemoryCache>(serviceProvider =>
            {
                var options = serviceProvider.GetRequiredService<IOptions<MemoryCacheOptions>>().Value;
                return new ExtendedMemoryCache(options);
            });

            return services;
        }
    }
}
