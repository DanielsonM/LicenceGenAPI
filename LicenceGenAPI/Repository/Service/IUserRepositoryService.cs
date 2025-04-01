using LicenceGenAPI.Models;

namespace LicenceGenAPI.Repository.Service
{
    public interface IUserRepositoryService
    {
        UserModel ValidateCredentials(UserVO objUser);
    }
}
