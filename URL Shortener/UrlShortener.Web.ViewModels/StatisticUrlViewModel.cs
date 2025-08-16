namespace UrlShortener.Web.ViewModels
{
    public class StatisticUrlViewModel
    {
        public int Views { get; set; }

        public List<TopVisitsViewModel> TopVisits { get; set; } = null!;
    }
}
