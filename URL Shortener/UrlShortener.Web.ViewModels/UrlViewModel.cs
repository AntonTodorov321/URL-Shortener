namespace UrlShortener.Web.ViewModels
{
    public class UrlViewModel
    {
        public Guid Id { get; set; }

        public string LongUrl { get; set; } = null!;

        public string ShortUrl { get; set; } = null!;
    }
}
