using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LicenceGenAPI.Models
{
    public class LicenceModel
    {
        [Key]
        [Column("int_id")]
        public int intId { get; set; }

        [Column("str_user_name")]
        public string strUserName { get; set; } = string.Empty;

        [BindNever]
        [Column("str_status")]
        public string strStatus { get; set; } = string.Empty;

        [BindNever]
        [Column("str_licence_key")]
        public string strLicenceKey { get; set; } = string.Empty;

        [Column("str_date_request")]
        public string strDateRequest { get; set; } = string.Empty;

        [Column("str_date_expiration")]
        public string strDataExpiration { get; set; } = string.Empty;

        [Column("str_observation")]
        public string strObservation { get; set; } = string.Empty;
    }
}

