﻿using Beast.Characters;
using Beast.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Security;

namespace Beast.ConnectionContexts
{
    public class NewCharacterConnectionContext : ConnectionContextBase
    {
        public const string CreateCharacterActionIndexKey = "CreateCharacterActionIndex";

        readonly Dictionary<int, Func<string,Task<bool>>> _actions = new Dictionary<int, Func<string, Task<bool>>>();
        readonly Dictionary<int, ContentKeys> _prompts = new Dictionary<int, ContentKeys>();

        readonly ICharacterNameValidator _characterNameValidator;
        readonly IPasswordValidator _passwordValidator;

        ReservedName _name;
        SecureString _password;
        string _email;
        string _realName;

        public NewCharacterConnectionContext(IServiceProvider services, IConnection connection) 
            : base(services, connection)
        {
            _characterNameValidator = services.GetService<ICharacterNameValidator>();
            _passwordValidator = services.GetService<IPasswordValidator>();
            
            _actions[0] = CharacterName;
            _prompts[0] = ContentKeys.CreateCharacterName;

            _actions[1] = Password;
            _prompts[1] = ContentKeys.CreateCharacterPassword;

            _actions[2] = PasswordConfirm;
            _prompts[2] = ContentKeys.CreateCharacterPasswordConfirm;

            _actions[3] = Email;
            _prompts[3] = ContentKeys.CreateCharacterEmail;

            _actions[4] = RealNameQuestion;
            _prompts[4] = ContentKeys.CreateCharacterRealNameQuestion;
        }

        public override async Task ProcessInput(string input)
        {
            var index = Connection.GetValue(CreateCharacterActionIndexKey, _actions.FirstOrDefault().Key);

            if (_actions.TryGetValue(index, out Func<string,Task<bool>> action))
            {
                if (await action.Invoke(input))
                {
                    var nextIndex = index++;
                    if (_actions.ContainsKey(nextIndex))
                    {
                        Connection.SetValue(CreateCharacterActionIndexKey, nextIndex);
                        await PromptAction(nextIndex);
                    }
                    else
                    {
                        // Finished
                        await Complete();
                    }
                }
            }
            else
            {
                // Invalid steps??
                await Enter();
            }
        }

        protected override async Task Enter()
        {
            await Connection.SendAsync(Content.GetText(ContentKeys.CreateCharacterIntro));
            await PromptAction(Connection.GetValue(CreateCharacterActionIndexKey, _actions.FirstOrDefault().Key));
        }

        async Task PromptAction(int index)
        {
            if (_prompts.TryGetValue(index, out ContentKeys key))
            {
                await Connection.SendAsync(Content.GetText(key));   
            }
        }

        Task Complete()
        {
            return Task.CompletedTask;
        }

        async Task<bool> CharacterName(string input)
        {
            // Validate the name.
            if (!_characterNameValidator.Validate(input, out string err))
            {
                await Connection.SendErrorAsync(err);
                return false;
            }

            using (var userRepo = Services.GetService<IUserRepository>())
            {
                // Ensure it doesn't exist in the database.
                if (await userRepo.GetUserByName(input) != null)
                {
                    await Connection.SendErrorAsync(Content.GetText(ContentKeys.CharacterNameAlreadyTaken));
                    return false;
                }

                // Reserve the name for a short time.
                _name = await userRepo.ReserveName(input);
                if (_name == null)
                {
                    await Connection.SendErrorAsync(Content.GetText(ContentKeys.CharacterNameAlreadyTaken));
                    return false;
                }
            }

            return true;
        }

        async Task<bool> Password(string input)
        {
            if (!_passwordValidator.Validate(input, out string err))
            {
                await Connection.SendErrorAsync(err);
                return false;
            }

            _password = new SecureString();
            foreach (var c in input)
            {
                _password.AppendChar(c);
            }
            _password.MakeReadOnly();

            return true;
        }

        async Task<bool> PasswordConfirm(string input)
        {
            if (!string.Equals(input, _password.ToUnsecureString()))
            {
                await Connection.SendErrorAsync(Content.GetText(ContentKeys.PasswordsDoNotMatch));
                return false;
            }
            return true;
        }

        async Task<bool> Email(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                await Connection.SendErrorAsync(Content.GetText(ContentKeys.CreateCharacterEmailRequired));
                return false;
            }

            _email = input;
            return true;
        }

        Task<bool> RealNameQuestion(string input)
        {
            if (!string.IsNullOrEmpty(input) && input.ToLowerInvariant() == "y")
            {
                _actions[5] = RealName;
                _prompts[5] = ContentKeys.CreateCharacterPassword;
            }

            return Task.FromResult(true);
        }

        Task<bool> RealName(string input)
        {
            _realName = input;
            return Task.FromResult(true);
        }
    }
}
