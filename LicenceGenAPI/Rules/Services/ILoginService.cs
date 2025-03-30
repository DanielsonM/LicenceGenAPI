using LicenceGenAPI.Data.VO;

namespace LicenceGenAPI.Rules.Services
{
    public interface ILoginService
    {
        TokenVO? ValidateCredentials(UserVO objUserVO);

        TokenVO? ValidateCredentials(TokenVO objToken);

        bool RevokeToken(string userName);
    }
}
