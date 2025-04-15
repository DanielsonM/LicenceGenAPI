using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LicenceGenAPI.Models
{
    [Table("users")]
    public class UserModel
    {
        [Key]
        [Column("int_id")]
        public int intId { get; set; }

        [Column("str_user_name")]
        public string strUserName { get; set; } = string.Empty;

        [Column("str_full_name")]
        public string strFullName { get; set; } = string.Empty;

        [Column("str_password")]
        public string strPassword { get; set; } = string.Empty;

        [Column("str_refresh_token")]
        public string strRefreshToken { get; set; } = string.Empty;

        [Column("dtt_refresh_token_expire_time")]
        public DateTime dttRefreshTokenExpiryTime { get; set; }
    }
}
