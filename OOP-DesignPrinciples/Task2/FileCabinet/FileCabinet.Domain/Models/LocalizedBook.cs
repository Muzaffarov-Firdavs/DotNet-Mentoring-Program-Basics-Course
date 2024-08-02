namespace FileCabinet.Domain.Models
{
    public class LocalizedBook : Book
    {
        public string OriginalPublisher { get; set; }
        public string CountryOfLocalization { get; set; }
        public string LocalPublisher { get; set; }
    }
}
