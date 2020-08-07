using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using server.Domain.Security;

namespace server.Domain.Storages
{
    public interface IRefreshCollection
    {
        void Add(RefreshToken refreshToken);
        RefreshToken getToken(string token);
        void Revoke(string token);
    }
}
