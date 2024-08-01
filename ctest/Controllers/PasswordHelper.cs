using System.Security.Cryptography;
using System.Text;

namespace ctest.Controllers
{
    public class PasswordHelper
    {
         private static readonly byte[] key = Encoding.UTF8.GetBytes("A very secret key"); // Key should be kept secure

    public static string HashPassword(string password)
    {
        using (var hmac = new HMACSHA256(key))
        {
            byte[] hashedPassword = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedPassword);
        }
    }

    public static bool VerifyPassword(string enteredPassword, string storedHash)
    {
        using (var hmac = new HMACSHA256(key))
        {
            byte[] enteredPasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(enteredPassword));
            string enteredPasswordHashString = Convert.ToBase64String(enteredPasswordHash);
            return enteredPasswordHashString == storedHash;
        }
    }
    }
}
