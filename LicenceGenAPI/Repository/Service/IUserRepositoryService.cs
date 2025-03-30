using LicenceGenAPI.Data.VO;
using LicenceGenAPI.Models;

namespace LicenceGenAPI.Repository.Service
{
    public interface IUserRepositoryService
    {
        UserModel? ValidateCredentials(UserVO user);

        UserModel? ValidateCredentials(string username);

        bool RevokeToken(string username);

        UserModel? RefreshUserInfo(UserModel user);


    }
}
