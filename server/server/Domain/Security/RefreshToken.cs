using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace server.Domain.Security
{
    public class RefreshToken : JsonWebToken
    {
        public string UserId { get; private set; }
        public RefreshToken(string token, long expiration, string userId): base(token, expiration)
        {
            UserId = userId;
        }
    }
}
