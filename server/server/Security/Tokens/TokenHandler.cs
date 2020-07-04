using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using server.Domain.Models;
using server.Domain.Security;
using server.Domain.Security.IPasswordHasher;
using server.Security.Hashing;

namespace server.Security.Tokens
{
    public class TokenHandler : ITokenHandler
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly TokenOptions _tokenOptions;
        private readonly SigningConfiguration _signingConfiguration;
        private readonly ISet<RefreshToken> _refreshTokens = new HashSet<RefreshToken>();
        public TokenHandler(IOptions<TokenOptions>tokenOptionsSnapshot, SigningConfiguration signingConfiguration, IPasswordHasher passwordHasher)
        {
            _passwordHasher = passwordHasher;
            _tokenOptions = tokenOptionsSnapshot.Value;
            _signingConfiguration = signingConfiguration;
        }

        public AccessToken CreateAccessToken(User user)
        {
            var refreshToken = BuildRefreshToken();
            var accessToken = BuildAccessToken(user, refreshToken);
            _refreshTokens.Add(refreshToken);

            return accessToken;
        }

        private AccessToken BuildAccessToken(User user, RefreshToken refreshToken)
        {
            var accessTokenExpiration = DateTime.UtcNow.AddSeconds(_tokenOptions.AccessTokenExpiration);
            var securityToken = new JwtSecurityToken(
                issuer: _tokenOptions.Issuer,
                audience: _tokenOptions.Audience,
                claims: new List<Claim>(),
                expires: accessTokenExpiration,
                notBefore: DateTime.UtcNow,
                signingCredentials: _signingConfiguration.SigningCredentials
                );

            var handler = new JwtSecurityTokenHandler();
            var accessToken = handler.WriteToken(securityToken);

            return new AccessToken(accessToken, accessTokenExpiration.Ticks, refreshToken);
        }

        private RefreshToken BuildRefreshToken()
        {
            var refreshToken = new RefreshToken(
                token: _passwordHasher.GetHashedPassword(Guid.NewGuid().ToString()),
                expiration: DateTime.UtcNow.AddSeconds(_tokenOptions.RefreshTokenExpiration).Ticks
                );
            return refreshToken;
        }

        public void RevokeRefreshToken(string token)
        {
            throw new NotImplementedException();
        }

        public RefreshToken TakeRefreshToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                return null;
            }

            var refreshToken = _refreshTokens.SingleOrDefault(refreshToken => refreshToken.Token == token);
            if (refreshToken != null)
            {
                _refreshTokens.Remove(refreshToken);
            }
            return refreshToken;
        }
    }
}
