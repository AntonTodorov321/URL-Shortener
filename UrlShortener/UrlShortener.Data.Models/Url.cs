namespace UrlShortener.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using static Common.GeneralApplicationConstants;

    public class Url
    {
        public Url()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        [Required]
        [MaxLength(UrlMaxLength)]
        [Url]
        public string OriginalUrl { get; set; } = null!;

        public string ShortUrl { get; set; } = null!;

        public string SecretCode { get; set; } = null!;

        [Column(TypeName = "DATE")]
        public DateTime CreatedOn { get; set; }
    }
}
