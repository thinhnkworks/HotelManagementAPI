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
        public bool VerifyPassword(string password, byte[] storedPasswordBytes, string secretKey)
        {
            // Chuyển đổi mật khẩu và khóa bí mật thành mảng byte
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] secretKeyBytes = Encoding.UTF8.GetBytes(secretKey);
            using (var hmac = new HMACSHA256(secretKeyBytes))
            {
                // Tính toán hash từ mật khẩu nhập vào
                byte[] hashedPasswordBytes = hmac.ComputeHash(passwordBytes);

                // So sánh dữ liệu đã hash từ mật khẩu nhập vào và dữ liệu đã lưu trữ
                return AreByteArraysEqual(hashedPasswordBytes, storedPasswordBytes);
            }
        }
        private bool AreByteArraysEqual(byte[] array1, byte[] array2)
        {
            if (array1.Length != array2.Length)
            {
                return false;
            }
            for (int i = 0; i < array1.Length; i++)
            {
                if (array1[i] != array2[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
