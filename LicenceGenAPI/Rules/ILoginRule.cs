using LicenceGenAPI.Data.VO;
using LicenceGenAPI.Models;

namespace LicenceGenAPI.Rules
{
    public interface ILoginRule
    {
        TokenVO? ValidateCredentials(UserVO? objUserVO);

        TokenVO? ValidateCredentials(TokenVO? objTokenVO);
    }
}
