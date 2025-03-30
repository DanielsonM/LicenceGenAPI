using System;
using System.Collections.Generic;
using System.Security.Claims;
using LicenceGenAPI.Configurations;
using LicenceGenAPI.Data.VO;
using LicenceGenAPI.Repository.Service;
using LicenceGenAPI.Rules.Services;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.Extensions.Options;

namespace LicenceGenAPI.TokenService.Implementation
{
    public class LoginRuleImplementation : ILoginService
    {
        private const string DATE_FORMAT = "yyyy-MM-dd HH:mm:ss";
        private readonly TokenConfiguration _configuration;
        private readonly IUserRepositoryService _repository;
        private readonly ITokenService _tokenService;

        public LoginRuleImplementation(IOptions<TokenConfiguration> configuration, IUserRepositoryService repository, ITokenService tokenService)
        {
            _configuration = configuration.Value;
            _repository = repository;
            _tokenService = tokenService;

            //// Verificações de valores da configuração
            //if (_configuration.Minutes <= 0)
            //{
            //    // Log de erro ou exceção
            //    throw new ArgumentException("Valor inválido para Minutes na configuração.");
            //}

            //if (_configuration.DaysToExpiry <= 0)
            //{
            //    // Log de erro ou exceção
            //    throw new ArgumentException("Valor inválido para DaysToExpiry na configuração.");
            //}
        }

        public TokenVO? ValidateCredentials(UserVO userCredentials)
        {
            var user = _repository.ValidateCredentials(userCredentials);
            if (user == null) return null;
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.strUserName)
            };

            var accessToken = _tokenService.GenerateAccessToken(claims);
            var refreshToken = _tokenService.GenerateRefreshToken();

            user.strRefreshToken = refreshToken;
            user.dttRefreshTokenExpiryTime = DateTime.Now.AddDays(_configuration.DaysToExpiry).ToUniversalTime();

            _repository.RefreshUserInfo(user);

            DateTime createDate = DateTime.Now;
            DateTime expirationDate = createDate.AddMinutes(_configuration.Minutes).ToUniversalTime();

            return new TokenVO(
                true,
                createDate.ToString(DATE_FORMAT),
                expirationDate.ToString(DATE_FORMAT),
                accessToken,
                refreshToken
                );
        }

        public TokenVO? ValidateCredentials(TokenVO token)
        {
            var accessToken = token.strAccessToken;
            var refreshToken = token.strRefreshToken;

            var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken);

            var username = principal.Identity?.Name;

            var user = _repository.ValidateCredentials(username!);

            if (user == null ||
                user.strRefreshToken != refreshToken ||
                user.dttRefreshTokenExpiryTime <= DateTime.Now) return null;

            accessToken = _tokenService.GenerateAccessToken(principal.Claims);
            refreshToken = _tokenService.GenerateRefreshToken();

            user.strRefreshToken = refreshToken;

            _repository.RefreshUserInfo(user);

            DateTime createDate = DateTime.Now;
            DateTime expirationDate = createDate.AddMinutes(_configuration.Minutes);

            return new TokenVO(
                true,
                createDate.ToString(DATE_FORMAT),
                expirationDate.ToString(DATE_FORMAT),
                accessToken,
                refreshToken
                );
        }

        public bool RevokeToken(string userName)
        {
            return _repository.RevokeToken(userName);
        }
    }
}