namespace Beast.Security
{
    public interface IPasswordValidator
    {
        bool Validate(string password, out string errorMessage);
    }
}
