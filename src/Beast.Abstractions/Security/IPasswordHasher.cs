namespace Beast.Security
{
    public interface IPasswordHasher
    {
        string GenerateSalt();
        string ComputeHash(string password, string salt);
        bool CheckPassword(string password, string hash, string salt);
    }
}
