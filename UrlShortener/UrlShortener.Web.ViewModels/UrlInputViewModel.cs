namespace UrlShortener.Web.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class UrlInputViewModel
    {
        [Required(ErrorMessage = "Url is required.")]
        [Url(ErrorMessage = "Please enter a valid URL.")]

        public string Url { get; set; } = null!;
    }
}
