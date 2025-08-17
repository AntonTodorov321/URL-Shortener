namespace UrlShortener.Services
{
    using System.Security.Cryptography;

    using Microsoft.EntityFrameworkCore;

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

        public async Task<Guid> ShorterUrl(string longUrl)
        {
            Url? url = await dbContext.Urls.FirstOrDefaultAsync(url => url.OriginalUrl == longUrl);

            if (url != null)
            {
                return url.Id;
            }

            string shortUrl = GenerateHash(8);
            string secretCode = GenerateHash(16);

            Url newUrl = new Url()
            {
                OriginalUrl = longUrl,
                ShortUrl = shortUrl,
                SecretCode = secretCode,
                CreatedOn = DateTime.UtcNow,
            };

            dbContext.Urls.Add(newUrl);
            await dbContext.SaveChangesAsync();

            return newUrl.Id;
        }

        public async Task<UrlViewModel> GetUrl(Guid id)
        {
            var url = await dbContext.Urls
                            .Where(url => url.Id == id)
                            .Select(url => new UrlViewModel()
                            {
                                Id = url.Id,
                                ShortUrl = url.ShortUrl,
                                SecretCode = url.SecretCode,
                            }).FirstOrDefaultAsync();

            return url!;
        }

        public async Task<string> GetOriginalUrl(Guid id)
        {
            string? longUrl = await dbContext.Urls.Where(url => url.Id == id)
                .Select(url => url.OriginalUrl)
                .FirstOrDefaultAsync();

            return longUrl!;
        }

        public async Task RecordAccess(string ip, Guid urlId)
        {
            await dbContext.UrlVisits.AddAsync(new UrlVisits()
            {
                IpAddress = ip,
                UrlId = urlId,
                VisitedAt = DateTime.UtcNow
            });

            await dbContext.SaveChangesAsync();
        }

        public async Task<StatisticUrlViewModel> GetStatistics(Guid id)
        {
            int views = await dbContext.UrlVisits
                .Where(uv => uv.UrlId == id && uv.VisitedAt.Day == DateTime.UtcNow.Day)
                .GroupBy(uv => uv.IpAddress)
                .CountAsync();

            List<TopVisitsViewModel> topVisits = await dbContext.UrlVisits
                .Where(uv => uv.UrlId == id)
                .GroupBy(uv => uv.IpAddress)
                .Select(g => new TopVisitsViewModel
                {
                    Ip = g.Key,
                    Count = g.Count()
                })
                .OrderByDescending(x => x.Count)
                .Take(10)
                .ToListAsync();

            return new StatisticUrlViewModel()
            {
                Views = views,
                TopVisits = topVisits
            };
        }

        private static string GenerateHash(int length)
        {
            byte[] bytes = new byte[length];
            RandomNumberGenerator.Fill(bytes);

            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            char[] result = new char[length];

            for (int i = 0; i < length; i++)
            {
                result[i] = chars[bytes[i] % chars.Length];
            }

            return new string(result);
        }
    }
}
