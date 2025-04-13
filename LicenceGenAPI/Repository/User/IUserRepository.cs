using LicenceGenAPI.Models;

namespace LicenceGenAPI.Repository.User
{
    public interface IUserRepository
    {
        UserModel? ValidateCredentials(UserVO objUserVO);

        UserModel? RefreshUserInfo(UserModel objUser);
    }
}
