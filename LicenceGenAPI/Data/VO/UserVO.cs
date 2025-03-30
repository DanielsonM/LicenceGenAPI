using System.ComponentModel.DataAnnotations.Schema;

namespace LicenceGenAPI.Data.VO
{
    public class UserVO
    {
        public string strUserName { get; set; } = string.Empty;

        public string strPassword { get; set; } = string.Empty;
    }
}