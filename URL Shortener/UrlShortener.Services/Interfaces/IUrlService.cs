namespace UrlShortener.Services.Interfaces
{
    using Web.ViewModels;

    public interface IUrlService
    {
        Task<Guid> ShorterUrl(string url);

        Task<UrlViewModel> GetUrl(Guid id);

        Task<string> GetOriginalUrl(Guid id);

        Task RecordAccess(string ip, Guid urlId);

        Task<StatisticUrlViewModel> GetStatistics(Guid id);
    }
}
