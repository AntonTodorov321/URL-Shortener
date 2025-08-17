namespace UrlShortener.Web.Infrastructure
{
    using System.Net;
    using System.ComponentModel.DataAnnotations;

    public class IpAddress : ValidationAttribute
    {
        protected override ValidationResult? 
            IsValid(object? value, ValidationContext validationContext)
        {
            if(value is string ip && IPAddress.TryParse(ip, out _))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("Invalid IP address.");
        }
    }
}
