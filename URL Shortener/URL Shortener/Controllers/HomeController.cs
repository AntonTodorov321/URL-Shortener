namespace UrlShortener.Controllers
{
    using System.Diagnostics;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using Models;
    using Services.Interfaces;
    using Web.ViewModels;

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
        public async Task<IActionResult> Shorter(string url)
        {
            Guid id = await urlService.ShorterUrl(url);

            return RedirectToAction("Shorter", new { id });
        }

        public async Task<IActionResult> Shorter(Guid id)
        {
            UrlViewModel viewModel = await urlService.GetUrlById(id);

            return View(viewModel);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
