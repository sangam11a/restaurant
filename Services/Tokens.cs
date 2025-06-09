using Stock.Models;

namespace Stock.Services
{
    public class Tokens
    {
        private readonly String refreshToken;
        public string GetRefreshToken()
        {
            var randomBytes = new byte[32];
            using(var rng = System.Security.Cryptography.RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
                string refreshToken = Convert.ToBase64String(randomBytes);
                return refreshToken;
            }
        }
    }
}
