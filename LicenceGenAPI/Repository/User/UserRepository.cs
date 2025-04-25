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

        public bool RevokeToken(string? strUserName)
        {
            if (string.IsNullOrWhiteSpace(strUserName)) return false;

            var objUser = _context.Users.FirstOrDefault(user =>
                user.strUserName.ToLower() == strUserName.ToLower());

            if (objUser == null) return false;

            objUser.strRefreshToken = null;

            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                // Log o erro ou trate conforme necessário
                return false;
            }

            return true;
        }

        public UserModel? ValidateCredentials(string? strUserName)
        {

            var objUser = _context.Users.SingleOrDefault(user => user.strUserName.Equals(strUserName));

            return objUser;
        }


        public UserModel? ValidateCredentials(UserVO? objUserVO)
        {
            if (objUserVO == null || string.IsNullOrWhiteSpace(objUserVO.strPassword) || string.IsNullOrWhiteSpace(objUserVO.strUserName))
            {
                return null;
            }

            string hashedPassword = ComputeHash(objUserVO.strPassword);

            var result = _context.Users.FirstOrDefault(user =>
                         user.strUserName.ToLower() == objUserVO.strUserName.ToLower() &&
                         user.strPassword == hashedPassword);

            return result;
        }

        private string ComputeHash(string strPassword)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(strPassword);
                byte[] hashedBytes = sha256.ComputeHash(inputBytes);

                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }
    }
}
