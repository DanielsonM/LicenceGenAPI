using System.Security.Claims;

namespace LicenceGenAPI.Services
{
    public interface IToken
    {

        string? GenerateAcessToken(IEnumerable<Claim>? objClaims);

        string? GenerateRefreshToken();

        ClaimsPrincipal? GetPrincipalExpiredToken(string? objToken);
    }
}
