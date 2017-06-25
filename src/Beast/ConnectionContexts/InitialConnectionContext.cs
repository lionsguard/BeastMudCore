using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Beast.ConnectionContexts
{
    public class InitialConnectionContext : ConnectionContextBase
    {
        readonly Dictionary<int, Func<Task>> _actions = new Dictionary<int, Func<Task>>();

        public InitialConnectionContext(IServiceProvider services, IConnection connection) 
            : base(services, connection)
        {
            _actions[1] = Login;
            _actions[2] = CreateCharacter;
            _actions[3] = Exit;
        }

        public override async Task ProcessInput(string input)
        {
            if (int.TryParse(input, out int selection) && _actions.TryGetValue(selection, out Func<Task> action))
            {
                await action.Invoke();
            }
            else
            {
                // Possible character name.
                await Login();
                await Connection.Context.ProcessInput(input);
            }
        }

        protected override async Task Enter()
        {
            await Connection.SendAsync(Content.GetText(ContentKeys.NewConnectionOptions));
        }

        Task Login()
        {
            Connection.Context = ContextFactory.CreateAuthenticationContext(Connection);
            return Task.CompletedTask;
        }

        Task CreateCharacter()
        {
            Connection.Context = ContextFactory.CreateNewCharacterContext(Connection);
            return Task.CompletedTask;
        }

        async Task Exit()
        {
            await Connection.CloseAsync();
        }
    }
}
