using FileCabinet.Domain.Models;

namespace FileCabinet.Service.Extensions
{
    public class CacheEntry
    {
        public Document Document { get; set; }
        public DateTime ExpirationTime { get; set; }
    }
}
