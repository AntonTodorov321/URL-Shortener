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

        public DbSet<Url> Urls { get; set; }

        public DbSet<Ip> Ips { get; set; }

        public DbSet<UrlIp> UrlIps { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UrlIp>()
                .HasKey(up => new { up.IpId, up.UrlId });

            base.OnModelCreating(builder);
        }
    }
}
