namespace UrlShortener.Services.Interfaces
{
    using Web.ViewModels;

    public interface IUrlService
    {
        Task<Guid> ShorterUrl(string url);

        Task<UrlViewModel> GetUrlById(Guid id);

        Task<string> GetOriginalUrlById(Guid id);

        Task RecordAccess(string ip, Guid urlId);
    }
}
