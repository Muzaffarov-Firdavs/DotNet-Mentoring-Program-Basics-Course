using FileCabinet.Domain.Models;
using FileCabinet.Service.Extensions;
using FileCabinet.Service.Interfaces;
using System.Runtime.Caching;

namespace FileCabinet.Service.Services
{
    public class CacheService : ICacheService
    {
        private readonly ObjectCache _cache = MemoryCache.Default;
        private readonly CacheItemPolicy _defaultPolicy = new CacheItemPolicy();

        public CacheEntry? Get(int documentNumber)
        {
            return _cache.Get(documentNumber.ToString()) as CacheEntry;
        }

        public void Add(int documentNumber, Document document, TimeSpan cacheDuration)
        {
            var key = documentNumber.ToString();
            var cacheEntry = new CacheEntry { Document = document };

            if (cacheDuration == TimeSpan.MaxValue)
            {
                // Infinite lifetime
                _defaultPolicy.Priority = CacheItemPriority.NotRemovable;
            }
            else if (cacheDuration == TimeSpan.Zero)
            {
                // Do not cache
                return;
            }
            else
            {
                _defaultPolicy.AbsoluteExpiration = DateTimeOffset.Now.Add(cacheDuration);
            }

            _cache.Set(key, cacheEntry, _defaultPolicy);
        }
    }
}