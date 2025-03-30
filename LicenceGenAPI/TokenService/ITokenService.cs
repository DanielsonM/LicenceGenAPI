using System.Security.Claims;

namespace LicenceGenAPI.TokenService
{
    public interface ITokenService
    {
       public  string GenerateAccessToken(IEnumerable<Claim> objClaims);

        public string GenerateRefreshToken();

        ClaimsPrincipal GetPrincipalFromExpiredToken(string strToken);
    }
}
