namespace WebApp.Handlers
{
    public class Hashing
    {
        private static string GetRandomSalt()
        {
            /// salt sepanjang 12
            return BCrypt.Net.BCrypt.GenerateSalt(12);
        }

        public static string HashPassword(string password)
        {
            /// bisa menerima 2 Parameter
            return BCrypt.Net.BCrypt.HashPassword(password,GetRandomSalt());
        }

        // fungsi ketika melakukan login
        public static bool ValidatePassword(string password,string correctHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, correctHash);
        }
    }
}
