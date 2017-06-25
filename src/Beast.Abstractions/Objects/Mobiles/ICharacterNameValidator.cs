namespace Beast.Objects.Mobiles
{
    public interface ICharacterNameValidator
    {
        bool Validate(string name, out string errorMessage);
    }
}
