namespace UrlShortener.Controllers
{
    using System.Threading.Tasks;

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

        public async Task<IActionResult> Statistic(Guid urlId)
        {
            StatisticUrlViewModel viewModel = await urlService.GetStatistics(urlId);
            return View();
        }

        //TODO: check for null
        public async Task<IActionResult> RedirectToLong(Guid urlId)
        {
            string originalUrl = await urlService.GetOriginalUrl(urlId);

            string userIp = HttpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault() ??
                            HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString()!;

            await urlService.RecordAccess(userIp, urlId);

            return Redirect(originalUrl);
        }

        [HttpPost]
        public async Task<IActionResult> Shorter(string url)
        {
            Guid id = await urlService.ShorterUrl(url);

            return RedirectToAction("Shorter", new { id });
        }

        //TOOD: check for null
        public async Task<IActionResult> Shorter(Guid id)
        {
            UrlViewModel viewModel = await urlService.GetUrl(id);

            return View(viewModel);
        }

        //TODO: Add error view
    }
}
