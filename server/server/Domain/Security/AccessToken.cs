﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace server.Domain.Security
{
    public class AccessToken: JsonWebToken
    {
        public string RefreshToken { get; private set; }
        public string UserId { get; private set; }

        public AccessToken(string token, long expiration, RefreshToken refreshToken, string userId): base(token, expiration)
        {
            if(refreshToken == null)
            {
                throw new ArgumentException("Specify a valid refresh token.");
            }
            UserId = userId;
            RefreshToken = refreshToken.Token;
        }
    }
}
