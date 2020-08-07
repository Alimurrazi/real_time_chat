using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server.Security.Tokens
{
    public class SigningConfiguration
    {
        public SecurityKey SecurityKey { get; }
        public SigningCredentials SigningCredentials { get; }

        public SigningConfiguration(string Key)
        {
            var KeyBytes = Encoding.ASCII.GetBytes(Key);

            SecurityKey = new SymmetricSecurityKey(KeyBytes);
            SigningCredentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256Signature);
        }

    }
}
