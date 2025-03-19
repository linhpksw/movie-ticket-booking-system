namespace G5_MovieTicketBookingSystem.Util
{
    public static class HashPassword
    {
        public static bool VerifyPassword(string password, string hashedPassword)
        {
            // So sánh mật khẩu nhập vào với mật khẩu đã mã hóa
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }

       public static string Hashing(string password)
        {
            // Mã hóa mật khẩu với salt ngẫu nhiên (work factor mặc định là 12)
            string hashed = BCrypt.Net.BCrypt.HashPassword(password);
            return hashed;
        }
        public static string GeneratePassword()
        {
            Random random = new Random();
            string password = string.Empty;

            for (int i = 0; i < 8; i++)
            {
                password += random.Next(0, 10).ToString();  // Tạo số ngẫu nhiên từ 0 đến 9 và chuyển thành chuỗi
            }

            return password;
        }
    }

}
