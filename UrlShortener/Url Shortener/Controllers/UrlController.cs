namespace UrlShortener.Controllers
{
    using System.Threading.Tasks;
    using System.Text.RegularExpressions;

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
                return RedirectToAction(nameof(Home));
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
                return RedirectToAction(nameof(Home));
            }

            string userIp = HttpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault() ??
                            HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString()!;

            if (!IsValidIp(userIp))
            {
                TempData[WarningMessage] = InvalidIpAddress;
                return RedirectToAction(nameof(Home));
            }

            try
            {
                await urlService.RecordAccess(userIp, urlId);
            }
            catch (Exception) { }

            return Redirect(originalUrl);
        }

        [HttpPost]
        public async Task<IActionResult> Shorter(UrlInputViewModel inputUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(nameof(Home), inputUrl);
            }

            Guid id = await urlService.ShorterUrl(inputUrl.Url);

            return RedirectToAction(nameof(Shorter), new { id });
        }

        public async Task<IActionResult> Shorter(Guid id)
        {
            string originalUrl = await urlService.GetOriginalUrl(id);

            if (originalUrl == null)
            {
                TempData[WarningMessage] = NotExistingId;
                return RedirectToAction(nameof(Home));
            }

            UrlViewModel viewModel = await urlService.GetUrl(id);

            return View(viewModel);
        }

        private bool IsValidIp(string ip)
        {
            return Regex.IsMatch(ip, @"^(\d{1,3}\.){3}\d{1,3}$") &&
                     ip.Split('.').All(number => int.Parse(number) <= 255);
        }
    }
}
