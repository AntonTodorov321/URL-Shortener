namespace UrlShortener.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using Services.Interfaces;
    using Web.ViewModels;

    public class UrlController : Controller
    {
        private readonly IUrlService urlService;

        public UrlController(IUrlService urlService)
        {
            this.urlService = urlService;
        }

        public IActionResult Home()
        {
            return View();
        }

        public IActionResult Details(string id)
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

        //TODO: Add error view
    }
}
