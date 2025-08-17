namespace UrlShortener.Data.Models
{
    //TODO: Add restriction and validations
    public class Url
    {
        public Url()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public string OriginalUrl { get; set; } = null!;

        public string ShortUrl { get; set; } = null!;

        public string SecretCode { get; set; } = null!;

        public DateTime CreatedOn { get; set; }
    }
}
