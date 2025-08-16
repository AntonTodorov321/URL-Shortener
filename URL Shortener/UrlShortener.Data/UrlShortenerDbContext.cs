namespace UrlShortener.Data
{
    using Microsoft.EntityFrameworkCore;

    using Models;

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

        public DbSet<Url> Urls { get; set; }

        public DbSet<UrlVisits> UrlVisits { get; set; }
    }
}
