using Microsoft.CodeAnalysis.Scripting;
using System.Text;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
namespace HotelManagementAPI.Helper
{
    public class Helper : IHelper
    {
        public bool CheckPassword(string password)
        {
            string regexPattern = @"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&*()_+{}[\]:;<>,.?~]).{8,}$";

            return Regex.IsMatch(password, regexPattern);
        }

        public byte[] HashPasswordToBytes(string password, string secretKey)
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] secretKeyBytes = Encoding.UTF8.GetBytes(secretKey);
            using (var hmac = new HMACSHA256(secretKeyBytes))
            {
                byte[] hashedPasswordBytes = hmac.ComputeHash(passwordBytes);
                return hashedPasswordBytes;
            }
        }
        public bool VerifyPassword(string password, byte[] hashedPasswordBytes)
        {
            string hashedPassword = Encoding.UTF8.GetString(hashedPasswordBytes);
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}
