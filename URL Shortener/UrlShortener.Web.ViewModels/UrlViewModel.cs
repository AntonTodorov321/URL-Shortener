namespace UrlShortener.Web.ViewModels
{
    public class UrlViewModel
    {
        public Guid Id { get; set; }

        public string ShortUrl { get; set; } = null!;

        public string SecretCode { get; set; } = null!;
    }
}
