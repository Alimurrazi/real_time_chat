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
using server.Domain.Storages;
using server.Storages;

namespace server.Security.Tokens
{
    public class TokenHandler : ITokenHandler
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly TokenOptions _tokenOptions;
        private readonly SigningConfiguration _signingConfiguration;
        private readonly IRefreshTokenCollection _refreshTokenCollection = new RefreshTokenCollection();
        public TokenHandler(IOptions<TokenOptions> tokenOptionsSnapshot, SigningConfiguration signingConfiguration, IPasswordHasher passwordHasher)
        {
            _passwordHasher = passwordHasher;
            _tokenOptions = tokenOptionsSnapshot.Value;
            _signingConfiguration = signingConfiguration;
        }

        public AccessToken CreateAccessToken(User user)
        {
            var refreshToken = BuildRefreshToken();
            var accessToken = BuildAccessToken(user, refreshToken);
            _refreshTokenCollection.Add(refreshToken);

            return accessToken;
        }

        private List<Claim> getCalims(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Role, user.Role)
            };
            return claims;
        }

        private AccessToken BuildAccessToken(User user, RefreshToken refreshToken)
        {
            var accessTokenExpiration = DateTime.UtcNow.AddSeconds(_tokenOptions.AccessTokenExpiration);
            var securityToken = new JwtSecurityToken(
                issuer: _tokenOptions.Issuer,
                audience: _tokenOptions.Audience,
                claims: this.getCalims(user),
                expires: accessTokenExpiration,
                notBefore: DateTime.UtcNow,
                signingCredentials: _signingConfiguration.SigningCredentials
                );

            var handler = new JwtSecurityTokenHandler();
            var accessToken = handler.WriteToken(securityToken);

            return new AccessToken(accessToken, accessTokenExpiration.Ticks, refreshToken, user.Id);
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

            // var refreshToken = _refreshTokens.SingleOrDefault(refreshToken => refreshToken.Token == token);
            var refreshToken = _refreshTokenCollection.getToken(token);
            if (refreshToken != null)
            {
                _refreshTokenCollection.Revoke(token);
            //    _refreshTokens.Remove(refreshToken);
            }
            return refreshToken;
        }
    }
}
