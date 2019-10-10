using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusCache.Services
{
    public class CacheService : ICacheService
    {
        private readonly ILogger<CacheService> _logger;
        private readonly IMemoryCache _memoryCache;

        public CacheService(ILogger<CacheService> logger, IMemoryCache memoryCache)
        {
            _logger = logger;
            _memoryCache = memoryCache;
        }

        public void Set(string key, object value)
        {
            _memoryCache.Set(key, value);
        }

        public object Get(string key)
        {
            return _memoryCache.Get(key);
        }

        public bool TryGet(string key, out object value)
        {
            return _memoryCache.TryGetValue(key, out value);
        }
    }
}
