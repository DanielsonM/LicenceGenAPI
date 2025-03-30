namespace LicenceGenAPI.Configurations
{
    public class TokenConfiguration
    {
        public string Audience { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty;
        public string Secret { get; set; } = string.Empty;
        public int Minutes{ get; set; }
        public int DaysToExpiry{ get; set; }
    }
}
