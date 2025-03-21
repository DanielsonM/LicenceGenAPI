using System.ComponentModel.DataAnnotations.Schema;

namespace LicenceGenAPI.Models
{
    [Table("users")]
    public class UserModel
    {
        [Column("int_id")]
        public int intId { get; set; }

        [Column("str_user_name")]
        public string strUserName { get; set; } = string.Empty;

        [Column("str_full_name")]
        public string strFullName { get; set; } = string.Empty;

        [Column("strPassword")]
        public string strPassword { get; set; } = string.Empty;

        [Column("str_refresh_token")]
        public string strRefreshToken { get; set; } = string.Empty;

        [Column("str_refresh_token_expire_time")]
        public string strRefreshTokenExpiryTime { get; set; } = string.Empty;
    }
}
