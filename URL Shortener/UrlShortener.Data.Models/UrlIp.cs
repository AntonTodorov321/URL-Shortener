namespace UrlShortener.Data.Models
{
    public class UrlIp
    {
        public Guid UrlId { get; set; }

        public Guid IpId { get; set; }

        public int TimesOpened { get; set; }
    }
}
