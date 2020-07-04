using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using server.Domain.Security;
using server.Domain.Security.IPasswordHasher;

namespace server.Security.Hashing
{
    public class PasswordHasher: IPasswordHasher
    {
        public string GetHashedPassword(string password)
        {
            string defaultSalt = "NZsP6NnmfBuYeJrrAKNuVQ==";
            byte[] salt = Encoding.ASCII.GetBytes(defaultSalt);

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return hashed;
        }
    }
}
