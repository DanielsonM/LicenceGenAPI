using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LicenceGenAPI.Data.VO
{
    public class LicenceVO
    {
        public int intId { get; set; }

        [JsonPropertyName("")]
        public string strUserName { get; set; } = string.Empty;

        [JsonIgnore]
        public string strStatus { get; set; } = string.Empty;

        public string strLicenceKey { get; set; } = string.Empty;

        public string strDateRequest { get; set; } = string.Empty;

        public string strDataExpiration { get; set; } = string.Empty;

        public string strObservation { get; set; } = string.Empty;

    }
}
