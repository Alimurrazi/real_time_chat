using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using server.Domain.Models;

namespace server.Domain.Security
{
    public interface ITokenHandler
    {
        AccessToken CreateAccessToken(User user);
        RefreshToken TakeRefreshToken(string token);
        void RevokeRefreshToken(string token);
    }
}
