using SSO.Interfaces;
using System;
using System.Runtime.Caching;

namespace SSO.Service.Logic
{
    /// <summary>
    /// Caching implementation using Memory Cache
    /// </summary>
    public class MemoryCacheManagement : ICacheManagement
    {
        private readonly System.Runtime.Caching.MemoryCache _cache;

        public MemoryCacheManagement()
        {
            _cache = MemoryCache.Default;
        }

        /// <summary>
        /// Remove a cached object
        /// </summary>
        /// <param name="key"></param>
        public void Expire(string key)
        {
            _cache.Remove(key);
        }

        /// <summary>
        /// Retrieve cached object based on key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object GetOnly(string key)
        {
            return _cache.Get(key);
        }

        /// <summary>
        /// Add new object to the cache
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        public void SetOnly(string key, object data)
        {
            _cache.Add(key, data, DateTimeOffset.UtcNow.AddMinutes(30));
        }
    }
}
