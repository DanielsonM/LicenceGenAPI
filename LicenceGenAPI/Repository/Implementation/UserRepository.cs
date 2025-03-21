using System.Text;
using LicenceGenAPI.DbConnection;
using LicenceGenAPI.Models;
using System.Security.Cryptography;
using System;

namespace LicenceGenAPI.Repository.Implementation
{
    public class UserRepository
    {
        private readonly PostgreDbContext _dbContext;

        public UserRepository(PostgreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public UserModel? ValidateCredentials(UserVO objUser)
        {
            var t = SHA256.Create();

            using (var sha256 = SHA256.Create())
            {
                var hashedPassword = ComputeHash(objUser.strPassword, sha256);

                return _dbContext.Users.FirstOrDefault(u =>
                    u.strUserName == objUser.strUserName && u.strPassword == hashedPassword);
            }
        }

        private string ComputeHash(string strInput, SHA256 algorithm)
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(strInput);
            byte[] hashedBytes = algorithm.ComputeHash(inputBytes);

            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }
    }
}