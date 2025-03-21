using System.ComponentModel.DataAnnotations.Schema;

namespace LicenceGenAPI.Models
{
    public class UserVO
    {
        public int intId { get; set; }

        public string strUserName { get; set; } = string.Empty;

        public string strFullName { get; set; } = string.Empty;

        public string strPassword { get; set; } = string.Empty;

        public string strRefreshToken { get; set; } = string.Empty;

        public string strRefreshTokenExpiryTime { get; set; } = string.Empty;
    }
}
