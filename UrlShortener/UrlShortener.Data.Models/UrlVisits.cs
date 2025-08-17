namespace UrlShortener.Data.Models
{
    public class UrlVisits
    {
        public UrlVisits()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public Guid UrlId { get; set; }

        public Url Url { get; set; } = null!;

        public string IpAddress { get; set; } = null!;

        public DateTime VisitedAt { get; set; }
    }
}
