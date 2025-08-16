namespace UrlShortener.Data.Models
{
    public class Url
    {
        public Url()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public string OriginalUrl { get; set; } = null!;

        public string ShortCode { get; set; } = null!;

        public int Views { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
