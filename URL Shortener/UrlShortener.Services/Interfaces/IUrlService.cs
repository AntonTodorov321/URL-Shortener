namespace UrlShortener.Services.Interfaces
{
    using Web.ViewModels;

    public interface IUrlService
    {
        Task<LongUrlViewModel> ShorterUrl(string url);
    }
}
