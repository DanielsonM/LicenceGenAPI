using System.Security.Cryptography;
using System.Text;
using LicenceGenAPI.DbConnection;
using LicenceGenAPI.Models;

namespace LicenceGenAPI.Repository.User
{
    public class UserRepository : IUserRepository
    {
        private readonly PostgreDbContext _context;

        public UserRepository(PostgreDbContext context)
        {
            _context = context;
        }

        public UserModel? RefreshUserInfo(UserModel objUser)
        {
            var wantendUser = _context.Users.Any(user => user.intId.Equals(objUser.intId));

            if (!wantendUser) return null;

            var result = _context.Users.SingleOrDefault(p => p.intId.Equals(objUser.intId));

            if ((result != null))
            {
                try
                {
                    objUser.dttRefreshTokenExpiryTime = objUser.dttRefreshTokenExpiryTime.ToUniversalTime();

                    _context.Entry(result).CurrentValues.SetValues(objUser);
                    _context.SaveChanges();
                }
                catch (Exception)
                {

                    throw;
                }
            }

            return result;

        }

        public UserModel? ValidateCredentials(UserVO objUserVO)
        {
            var user = _context.Users.FirstOrDefault(u => u.strUserName == objUserVO.strUserName);

            if (user != null)
            {
                using (var sha256 = SHA256.Create())
                {
                        var passWord = this.ComputeHash(objUserVO.strPassword, sha256);

                    var result = _context.Users.FirstOrDefault(user => user.strUserName.Equals(objUserVO.strUserName) && user.strPassword.Equals(passWord));

                    return result;
                }
            }

            return null;
        }

        public UserModel? ValidateCredentials(string strUserName)
        {

            var objUser = _context.Users.SingleOrDefault(user => user.strUserName.Equals(strUserName));

            return objUser;
        }

        private object ComputeHash(string strPassword, SHA256 sha256)
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(strPassword);
            byte[] hashedBytes = sha256.ComputeHash(inputBytes);

            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }
    }
}
