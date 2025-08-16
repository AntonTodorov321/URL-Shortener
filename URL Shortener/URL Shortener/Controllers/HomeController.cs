namespace UrlShortener.Controllers
{
    using System.Diagnostics;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using Models;
    using Web.ViewModels;
    using Services.Interfaces;

    public class HomeController : Controller
    {
        private readonly IUrlService urlService;

        public HomeController(IUrlService urlService)
        {
            this.urlService = urlService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Shortener(string url)
        {
            LongUrlViewModel longUrl = await urlService.ShorterUrl(url);
            return null;
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
