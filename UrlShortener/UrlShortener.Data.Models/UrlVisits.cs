namespace UrlShortener.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class UrlVisits
    {
        public UrlVisits()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public Guid UrlId { get; set; }

        public Url Url { get; set; } = null!;

        [Required]
        public string IpAddress { get; set; } = null!;

        [Column(TypeName = "DATE")]
        public DateTime VisitedOn { get; set; }
    }
}
