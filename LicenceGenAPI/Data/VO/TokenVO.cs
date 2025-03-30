using Microsoft.AspNetCore.Http.HttpResults;

namespace LicenceGenAPI.Data.VO
{
    public class TokenVO
    {
        public TokenVO(bool authenticated, string created, string expiration, string accessToken, string refreshToken)
        {
            booAuthenticated = authenticated;
            dttCreate = created;
            strExpiration = expiration;
            strAccessToken = accessToken;
            strRefreshToken = refreshToken;
        }
        public bool booAuthenticated { get; set; }

        public string strExpiration { get; set; } = string.Empty;

        public string strAccessToken { get; set; } = string.Empty;

        public string strRefreshToken { get; set; } = string.Empty;

        public string dttCreate { get; set; } = string.Empty;
    }
}
