namespace FileCabinet.Domain.Models
{
    public class Book : Document
    {
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string[] Authors { get; set; }
        public int NumberOfPages { get; set; }
        public string Publisher { get; set; }
        public DateTime DatePublished { get; set; }
    }
}
