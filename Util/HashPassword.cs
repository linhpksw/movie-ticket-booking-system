namespace G5_MovieTicketBookingSystem.Util
{
    public class HashPassword
    {
        static bool VerifyPassword(string password, string hashedPassword)
        {
            // So sánh mật khẩu nhập vào với mật khẩu đã mã hóa
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }

        static string HashPassword(string password)
        {
            // Mã hóa mật khẩu với salt ngẫu nhiên (work factor mặc định là 12)
            string hashed = BCrypt.Net.BCrypt.HashPassword(password);
            return hashed;
        }
    }

}
