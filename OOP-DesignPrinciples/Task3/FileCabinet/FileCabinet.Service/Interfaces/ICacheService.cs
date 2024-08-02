using FileCabinet.Domain.Models;
using FileCabinet.Service.Extensions;

namespace FileCabinet.Service.Interfaces
{
    public interface ICacheService
    {
        CacheEntry? Get(int documentNumber);
        void Add(int documentNumber, Document document, TimeSpan cacheDuration);
    }
}