namespace UrlShortener
{
    using Microsoft.EntityFrameworkCore;

    using Data;
    using Services;
    using Services.Interfaces;

    public class Program
    {
        public static async Task Main(string[] args)
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

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error/500");
                app.UseStatusCodePagesWithRedirects("/Home/Error?statusCode={0}");

                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Url}/{action=Home}");

            await app.RunAsync();
        }
    }
}
