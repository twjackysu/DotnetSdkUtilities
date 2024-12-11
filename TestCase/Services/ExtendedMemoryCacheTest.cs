using DotnetSdkUtilities.Services;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestCase.Services
{
    [TestClass]
    public class ExtendedMemoryCacheTest
    {
        [TestMethod]
        public void ShouldGetKeys_NoExpiration()
        {
            var cache = new ExtendedMemoryCache(new MemoryCacheOptions());

            cache.Set("A", 1);  // Valid without expiration
            var keys = cache.Keys;

            Assert.AreEqual(1, keys.Count);
            Assert.IsTrue(keys.Contains("A"));
        }

        [TestMethod]
        public void ShouldGetKeys_AbsoluteExpiration()
        {
            var cache = new ExtendedMemoryCache(new MemoryCacheOptions());

            cache.Set("B", 2, DateTimeOffset.UtcNow.AddMinutes(-5)); // Expired
            cache.Set("C", 3, DateTimeOffset.UtcNow.AddMinutes(5));  // Not expired
            var keys = cache.Keys;

            Assert.AreEqual(1, keys.Count); // Only "C" should remain
            Assert.IsTrue(keys.Contains("C"));
            Assert.IsFalse(keys.Contains("B")); // Expired
        }

        [TestMethod]
        public void ShouldGetKeys_RelativeExpiration()
        {
            var cache = new ExtendedMemoryCache(new MemoryCacheOptions());

            cache.Set("D", 4, TimeSpan.FromSeconds(1)); // Expires soon
            System.Threading.Thread.Sleep(2000);       // Wait to ensure expiration
            cache.Set("E", 5, TimeSpan.FromMinutes(5)); // Not expired
            var keys = cache.Keys;

            Assert.AreEqual(1, keys.Count); // Only "E" should remain
            Assert.IsTrue(keys.Contains("E"));
            Assert.IsFalse(keys.Contains("D")); // Expired
        }

        [TestMethod]
        public void ShouldGetKeys_ClearExpiredEntries()
        {
            var cache = new ExtendedMemoryCache(new MemoryCacheOptions());

            cache.Set("F", 6);  // Valid without expiration
            cache.Set("G", 7, TimeSpan.FromSeconds(1)); // Expires soon
            System.Threading.Thread.Sleep(2000);       // Wait to ensure expiration
            var keysBeforeAccess = cache.Keys; // Access keys before manually checking expired

            Assert.AreEqual(1, keysBeforeAccess.Count); // Expired entries should be automatically removed
            Assert.IsTrue(keysBeforeAccess.Contains("F"));
            Assert.IsFalse(keysBeforeAccess.Contains("G")); // Expired
        }

        [TestMethod]
        public void ShouldGetKeys_ManuallyAccessExpiredEntries()
        {
            var cache = new ExtendedMemoryCache(new MemoryCacheOptions());

            cache.Set("H", 8, TimeSpan.FromSeconds(1)); // Expires soon
            System.Threading.Thread.Sleep(2000);       // Wait to ensure expiration
            var value = cache.Get<int?>("H"); // Try to access expired entry
            var keys = cache.Keys;

            Assert.AreEqual(0, keys.Count); // All expired entries should be removed
            Assert.IsNull(value);           // Expired entry should return null
        }
    }
}
