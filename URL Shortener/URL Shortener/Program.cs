namespace UrlShortener
{
    using Microsoft.EntityFrameworkCore;

    using UrlShortener.Data;
    using UrlShortener.Services;
    using UrlShortener.Services.Interfaces;

    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            string connectionString =
                builder.Configuration.GetConnectionString("DefaultConnection") ??
                        throw new InvalidOperationException(
                            "Connection string 'DefaultConnection' not found.");

            builder.Services.AddDbContext<UrlShortenerDbContext>(options =>
                    options.UseSqlServer(connectionString));

            builder.Services.AddControllersWithViews();

            builder.Services.AddScoped<IUrlService, UrlService>();

            WebApplication app = builder.Build();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
