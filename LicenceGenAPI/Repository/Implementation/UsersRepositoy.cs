using System.Security.Cryptography;
using System.Text;
using LicenceGenAPI.Data.VO;
using LicenceGenAPI.DbConnection;
using LicenceGenAPI.Models;
using LicenceGenAPI.Repository.Service;

namespace LicenceGenAPI.Repository.Implementation
{
    public class UsersRepository : IUserRepositoryService
    {
        private readonly PostgreDbContext _context;

        public UsersRepository(PostgreDbContext context)
        {
            _context = context;
        }

        public UserModel? ValidateCredentials(UserVO user)
        {
            var pass = ComputeHash(user.strPassword, SHA256.Create());

            var userInDb = _context.Users.FirstOrDefault(u => u.strUserName == user.strUserName);

            if (userInDb != null)
            {
                userInDb.strPassword = pass;
                _context.SaveChanges(); 
            }

            var validate = _context.Users.FirstOrDefault(u =>
                u.strUserName == user.strUserName &&
                u.strPassword == pass
            );

            return validate;
        }

        public UserModel? ValidateCredentials(string userName)
        {
            return _context.Users.SingleOrDefault(u => (u.strUserName == userName));
        }

        public bool RevokeToken(string userName)
        {
            var user = _context.Users.SingleOrDefault(u => (u.strUserName == userName));
            if (user is null) return false;
            user.strRefreshToken = string.Empty;
            _context.SaveChanges();
            return true;
        }

        public UserModel? RefreshUserInfo(UserModel user)
        {
            if (!_context.Users.Any(u => u.intUserId.Equals(user.intUserId))) return null;

            var result = _context.Users.SingleOrDefault(p => p.intUserId.Equals(user.intUserId));
            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(user);
                    _context.SaveChanges();
                    return result;
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return result;
        }

        private string ComputeHash(string input, HashAlgorithm algorithm)
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            byte[] hashedBytes = algorithm.ComputeHash(inputBytes);

            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower(); 
        }
    }
}