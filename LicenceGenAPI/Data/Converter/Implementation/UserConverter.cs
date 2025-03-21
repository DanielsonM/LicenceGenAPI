using LicenceGenAPI.Data.Converter.Contract;
using LicenceGenAPI.Models;

namespace LicenceGenAPI.Data.Converter.Implementation
{
    public class UserConverter : IParser<UserVO, UserModel>, IParser<UserModel, UserVO>
    {
        public UserModel Parse(UserVO objOrigin)
        {
            return new UserModel()
            {
                intId = objOrigin.intId,
                strUserName = objOrigin.strUserName,
                strFullName = objOrigin.strFullName,
                strPassword = objOrigin.strPassword,
                strRefreshToken = objOrigin.strRefreshToken,
                strRefreshTokenExpiryTime = objOrigin.strRefreshTokenExpiryTime,

            };
        }

        public List<UserModel> Parse(List<UserVO> lstOrigin)
        {
            return lstOrigin.Select(u => Parse(u)).ToList();
        }

        public UserVO Parse(UserModel objOrigin)
        {
            return new UserVO()
            {
                intId = objOrigin.intId,
                strUserName = objOrigin.strUserName,
                strFullName = objOrigin.strFullName,
                strPassword = objOrigin.strPassword,
                strRefreshToken = objOrigin.strRefreshToken,
                strRefreshTokenExpiryTime = objOrigin.strRefreshTokenExpiryTime,

            };
        }

        public List<UserVO> Parse(List<UserModel> lstOrigin)
        {
            return lstOrigin.Select(u => Parse(u)).ToList();
        }
    }
}
