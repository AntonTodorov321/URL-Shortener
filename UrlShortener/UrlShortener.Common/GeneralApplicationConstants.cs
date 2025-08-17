namespace UrlShortener.Common
{
    public static class GeneralApplicationConstants
    {
        public static int ShortUrlLength = 8;
        public static int SecretCodeLength = 16;

        public const string Base62Characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        public static string NotExistingId = "This id does not exist! Please select existing one.";
        public static string NotExistingUrl = 
            "This url does not exist. Please select an existing one";
    }
}
