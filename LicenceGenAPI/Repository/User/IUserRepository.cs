using LicenceGenAPI.Models;

namespace LicenceGenAPI.Repository.User
{
    public interface IUserRepository
    {
        UserModel? ValidateCredentials(UserVO? objUserVO);

        UserModel? ValidateCredentials(string? strUserName);

        public bool RevokeToken(string? strUserName);

        UserModel? RefreshUserInfo(UserModel objUser);
    }
}
