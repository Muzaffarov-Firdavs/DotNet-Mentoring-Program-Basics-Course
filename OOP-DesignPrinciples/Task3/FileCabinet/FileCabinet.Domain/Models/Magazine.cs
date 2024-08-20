namespace FileCabinet.Domain.Models
{
    public class Magazine : Document
    {
        public string Publisher { get; set; }
        public int ReleaseNumber { get; set; }
    }
}
