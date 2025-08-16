namespace UrlShortener.Services
{
    using System.Security.Cryptography;

    using Data;
    using Data.Models;
    using Interfaces;
    using Web.ViewModels;

    public class UrlService : IUrlService
    {
        private readonly UrlShortenerDbContext dbContext;

        public UrlService(UrlShortenerDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<LongUrlViewModel> ShorterUrl(string longUrl)
        {
            string shortUrl = GenerateShortenUrl();

            Url newUrl = new Url()
            {
                OriginalUrl = longUrl,
                CreatedOn = DateTime.Now,
                ShortCode = shortUrl
            };

            dbContext.Urls.Add(newUrl);
            await dbContext.SaveChangesAsync();

            return new LongUrlViewModel()
            {
                LongUrl = longUrl,
                ShortUrl = shortUrl
            };
        }

        private static string GenerateShortenUrl()
        {
            byte[] bytes = new byte[8];
            RandomNumberGenerator.Fill(bytes);

            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            char[] result = new char[8];

            for (int i = 0; i < 8; i++)
            {
                result[i] = chars[bytes[i] % chars.Length];
            }

            return new string(result);
        }
    }
}
