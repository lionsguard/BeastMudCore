using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Beast.Characters
{
    public class CharacterNameValidator : ICharacterNameValidator
    {
        public static Regex NameRegex = new Regex(@"[a-zA-Z\']");

        string _requirements;
        public string Requirements
        {
            get { return _requirements ?? (_requirements = _content.GetText(ContentKeys.CharacterNameRequirements)); }
        }

        readonly List<string> _reservedWords = new List<string>();
        public List<string> ReservedWords
        {
            get
            {
                if (_reservedWords.Count == 0)
                {
                    _reservedWords.AddRange(_content.GetText(ContentKeys.ReservedWords).Split(Environment.NewLine.ToCharArray()));
                }
                return _reservedWords;
            }
        }

        readonly IContentProvider _content;

        public CharacterNameValidator(IContentProvider content)
        {
            _content = content;
        }

        public bool Validate(string name, out string errorMessage)
        {
            if (string.IsNullOrEmpty(name) || name.Length < 3)
            {
                errorMessage = Requirements;
                return false;
            }

            if (!NameRegex.IsMatch(name))
            {
                errorMessage = Requirements;
                return false;
            }

            if (ReservedWords.Contains(name))
            {
                errorMessage = _content.GetText(ContentKeys.CharacterNameInvalid);
                return false;
            }

            errorMessage = null;
            return true;
        }
    }
}
