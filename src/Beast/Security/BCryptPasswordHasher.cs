namespace Beast.Security
{
    public class BCryptPasswordHasher : IPasswordHasher
    {
        public bool CheckPassword(string password, string hash, string salt)
        {
            return BCrypt.CheckPassword(password, hash, salt);
        }

        public string ComputeHash(string password, string salt)
        {
            return BCrypt.HashPassword(password, salt);
        }

        public string GenerateSalt()
        {
            return BCrypt.GenerateSalt();
        }
    }
}
