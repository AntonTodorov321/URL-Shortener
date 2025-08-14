namespace UrlShortener.Data
{
    using Microsoft.EntityFrameworkCore;

    using UrlShortener.Data.Models;

    public class UrlShortenerDbContext : DbContext
    {
        public UrlShortenerDbContext(DbContextOptions<UrlShortenerDbContext> options)
            : base(options)
        {
            if (!this.Database.IsRelational())
            {
                this.Database.EnsureCreated();
            }
        }

        public DbSet<Url> Urls { get; set; } = null!;
    }
}
