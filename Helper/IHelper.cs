namespace HotelManagementAPI.Helper
{
    public interface IHelper
    {
        bool CheckPassword(string password);
        byte[] HashPasswordToBytes(string password, string secretKey);
        bool VerifyPassword(string password, byte[] storedPasswordBytes, string secretKey);
    }
}
