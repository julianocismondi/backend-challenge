using System.Security.Cryptography;
using System.Text;

namespace backend_challenge.Helpers
{
    public static class HashPass
    {
        public static string HashPassword(string password)
        {
            using (var sha256 = new SHA256Managed())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                var hash = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
                return hash;
            }
        }

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            using (var sha256 = new SHA256Managed())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                var hash = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();

                return hash == hashedPassword;
            }
        }
    }
}
