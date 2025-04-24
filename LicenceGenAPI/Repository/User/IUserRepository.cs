using LicenceGenAPI.Models;

namespace LicenceGenAPI.Repository.User
{
    public interface IUserRepository
    {
        UserModel? ValidateCredentials(UserVO? objUserVO);

        UserModel? ValidateCredentials(string? strUserName);

        UserModel? RefreshUserInfo(UserModel objUser);
    }
}
