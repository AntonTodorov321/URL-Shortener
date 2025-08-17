namespace UrlShortener.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using Services.Interfaces;
    using Web.ViewModels;

    using static Common.NotificationMessagesConstants;
    using static Common.GeneralApplicationConstants;

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
            string originalUrl = await urlService.GetOriginalUrl(urlId);

            if (originalUrl == null)
            {
                TempData[WarningMessage] = NotExistingId;
                return RedirectToAction("Home");
            }

            StatisticUrlViewModel viewModel = await urlService.GetStatistics(urlId);
            return View(viewModel);
        }

        public async Task<IActionResult> RedirectToLong(Guid urlId)
        {
            string originalUrl = await urlService.GetOriginalUrl(urlId);

            if (originalUrl == null)
            {
                TempData[WarningMessage] = NotExistingUrl;
                return RedirectToAction("Home");
            }

            string userIp = HttpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault() ??
                            HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString()!;

            try
            {
                await urlService.RecordAccess(userIp, urlId);
            }
            catch (Exception) { }

            return Redirect(originalUrl);
        }

        [HttpPost]
        public async Task<IActionResult> Shorter(string url)
        {
            Guid id = await urlService.ShorterUrl(url);

            return RedirectToAction("Shorter", new { id });
        }

        public async Task<IActionResult> Shorter(Guid id)
        {
            string originalUrl = await urlService.GetOriginalUrl(id);

            if (originalUrl == null)
            {
                TempData[WarningMessage] = NotExistingId;
                return RedirectToAction("Home");
            }

            UrlViewModel viewModel = await urlService.GetUrl(id);

            return View(viewModel);
        }
    }
}
