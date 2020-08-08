using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using server.Domain.Security;
using server.Domain.Storages;

namespace server.Storages
{
    public class RefreshTokenCollection : IRefreshTokenCollection
    {
        private readonly ISet<RefreshToken> _refreshTokens = new HashSet<RefreshToken>();
        public void Add(RefreshToken refreshToken)
        {
            lock (_refreshTokens)
            {
                _refreshTokens.Add(refreshToken);
            }
        }

        public RefreshToken getToken(string token)
        {
            var refreshToken = _refreshTokens.SingleOrDefault(refreshToken => refreshToken.Token == token);
            return refreshToken;
        }

        public void Revoke(string token)
        {
            lock (_refreshTokens)
            {
                var refreshToken = this.getToken(token);
                if (refreshToken != null)
                {
                    _refreshTokens.Remove(refreshToken);
                }
            }
        }
    }
}
