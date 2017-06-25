using System.Text.RegularExpressions;

namespace Beast.Security
{
    public class PasswordValidator : IPasswordValidator
    {
        public static readonly Regex UpperRegex = new Regex(@"[A-Z]+");
        public static readonly Regex LowerRegex = new Regex(@"[a-z]+");
        public static readonly Regex NumberRegex = new Regex(@"\d+");
        
        string _requirements;
        public string Requirements
        {
            get { return _requirements ?? (_requirements = _content.GetText(ContentKeys.PasswordRequirements)); }
        }

        readonly IContentProvider _content;

        public PasswordValidator(IContentProvider content)
        {
            _content = content;
        }

        public bool Validate(string password, out string errorMessage)
        {
            if (string.IsNullOrEmpty(password) || password.Length < 8)
            {
                errorMessage = _content.GetText(ContentKeys.PasswordRequiredLength);
                return false;
            }

            if (!UpperRegex.IsMatch(password))
            {
                errorMessage = Requirements;
                return false;
            }

            if (!LowerRegex.IsMatch(password))
            {
                errorMessage = Requirements;
                return false;
            }

            if (!NumberRegex.IsMatch(password))
            {
                errorMessage = Requirements;
                return false;
            }

            errorMessage = null;
            return true;
        }
    }
}
