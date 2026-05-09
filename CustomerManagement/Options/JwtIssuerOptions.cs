namespace CustomerManagement.Options
{
    public class JwtIssuerOptions
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }

        public int TokenExpiry { get; set; }

        public string Key { get; set; }
    }
}
