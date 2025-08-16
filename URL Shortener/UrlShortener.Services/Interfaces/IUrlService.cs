namespace UrlShortener.Services.Interfaces
{
    using Web.ViewModels;

    public interface IUrlService
    {
        Task<Guid> ShorterUrl(string url);

        Task<UrlViewModel> GetUrlById(Guid id);

        Task<string> GetOriginalUrlByShortUrl(string shortUrl);
    }
}
