using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using LicenceGenAPI.Configurarions;
using LicenceGenAPI.Data.VO;
using LicenceGenAPI.Models;
using LicenceGenAPI.Repository.User;
using LicenceGenAPI.Services;

namespace LicenceGenAPI.Rules
{
    public class LoginRule : ILoginRule
    {
        private const string DATE_FORMAT = "yyyy-MM-dd HH:mm:ss";
        private TokenConfiguration _tokenConfiguration;
        private IUserRepository _userRepository;
        private readonly IToken _token;

        public LoginRule(TokenConfiguration tokenConfiguration, IUserRepository userRepository, IToken token)
        {
            _tokenConfiguration = tokenConfiguration;
            _userRepository = userRepository;
            _token = token;
        }

        public TokenVO ValidateCredentials(UserVO objUserVO)
        {

            var user = _userRepository.ValidateCredentials(objUserVO);

            if (user == null) return null;

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                new Claim(JwtRegisteredClaimNames.UniqueName,user.strUserName),
            };


            var accessToken = _token.GenerateAcessToken(claims);
            var refreshToken = _token.GenerateRefreshToken();

            user.strRefreshToken = refreshToken;
            user.dttRefreshTokenExpiryTime = DateTime.Now.AddDays(_tokenConfiguration.DaysToExpirate);

            _userRepository.RefreshUserInfo(user);

            DateTime createDate = DateTime.Now;
            DateTime expirationDate = createDate.AddMinutes(_tokenConfiguration.Minutes);


            return new TokenVO(true, createDate.ToString(DATE_FORMAT), expirationDate.ToString(DATE_FORMAT), accessToken,refreshToken);
        }
    }
}
