namespace FileCabinet.Domain.Models
{
    public class Patent : Document
    {
        public DateTime ExpirationDate { get; set; }
        public string UniqueId { get; set; }
    }
}
