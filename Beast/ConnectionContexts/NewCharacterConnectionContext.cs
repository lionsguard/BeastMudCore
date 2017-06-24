using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Beast.ConnectionContexts
{
    public class NewCharacterConnectionContext : ConnectionContextBase
    {
        public const string CreateCharacterActionIndexKey = "CreateCharacterActionIndex";

        readonly Dictionary<int, Func<string,Task<bool>>> _actions = new Dictionary<int, Func<string, Task<bool>>>();
        readonly Dictionary<int, ContentKeys> _prompts = new Dictionary<int, ContentKeys>();

        public NewCharacterConnectionContext(IServiceProvider services, IConnection connection) 
            : base(services, connection)
        {
            _actions[0] = CharacterName;
            _prompts[0] = ContentKeys.CreateCharacterName;

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

        Task<bool> CharacterName(string input)
        {
            return Task.FromResult(false);
        }
    }
}
