using FileCabinet.Domain.Models;
using FileCabinet.Service.Extensions;
using FileCabinet.Service.Interfaces;
using System.Collections.Concurrent;

namespace FileCabinet.Service.Services
{
    public class CacheService : ICacheService
    {
        private readonly ConcurrentDictionary<int, CacheEntry> _cache = new();

        public CacheEntry? Get(int documentNumber)
        {
            if (_cache.TryGetValue(documentNumber, out CacheEntry cacheEntry))
            {
                if (cacheEntry.ExpirationTime > DateTime.Now)
                {
                    return cacheEntry;
                }
                else
                {
                    _cache.TryRemove(documentNumber, out _);
                }
            }
            return null;
        }

        public void Add(int documentNumber, Document document, TimeSpan cacheDuration)
        {
            var expirationTime = cacheDuration == TimeSpan.MaxValue
                ? DateTime.MaxValue
                : DateTime.Now.Add(cacheDuration);

            var cacheEntry = new CacheEntry
            {
                Document = document,
                ExpirationTime = expirationTime
            };

            _cache[documentNumber] = cacheEntry;
        }
    }
}
