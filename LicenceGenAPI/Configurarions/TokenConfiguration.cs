namespace LicenceGenAPI.Configurarions
{
    public class TokenConfiguration
    {
        public string Issuer { get; set; } = string.Empty;

        public string Audience { get; set; } = string.Empty;

        public string Secret { get; set; } = string.Empty;

        public int Minutes { get; set; }

        public int DaysToExpirate { get; set; }
    }
}
