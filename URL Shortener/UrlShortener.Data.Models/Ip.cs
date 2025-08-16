namespace UrlShortener.Data.Models
{
    public class Ip
    {
        public Ip()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public string UserIp { get; set; } = null!;
    }
}
