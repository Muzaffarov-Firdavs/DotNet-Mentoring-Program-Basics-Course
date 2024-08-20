namespace FileCabinet.Domain.Models
{
    public abstract class Document
    {
        public int DocumentNumber { get; set; }
        public string Title { get; set; }
        public string[] Authors { get; set; }
        public DateTime DatePublished { get; set; }
    }
}
