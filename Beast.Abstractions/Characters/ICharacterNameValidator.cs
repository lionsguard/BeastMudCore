namespace Beast.Characters
{
    public interface ICharacterNameValidator
    {
        bool Validate(string name, out string errorMessage);
    }
}
