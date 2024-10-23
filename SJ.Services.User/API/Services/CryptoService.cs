using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace API.Services
{
    public class CryptoService
    {
        public (string HashedPassword, string Salt) Hash(string password)
        {
            var salt = RandomNumberGenerator.GetBytes(128 / 8);

            string hashed = Convert.ToBase64String(
                KeyDerivation.Pbkdf2(
                password: password!,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8)
                );

            return (Convert.ToBase64String(salt), hashed);
        }
    }
}
