namespace UrlShortener.Data.Models
{
    public class Url
    {
        public int Id { get; set; }

        public string OriginalUrl { get; set; }

        public string ShortCode { get; set; }

        public int Views { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
