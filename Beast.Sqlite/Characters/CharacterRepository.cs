using Beast.Characters;
using System;
using System.Collections.Generic;
using System.Text;
using Beast.Security;
using System.Threading.Tasks;

namespace Beast.Sqlite.Characters
{
    public class CharacterRepository : RepositoryBase, ICharacterRepository
    {
        public CharacterRepository(SqliteContext context) 
            : base(context)
        {
        }

        public Task<ICharacter> GetCharacterById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ICharacter> GetCharacterByName(string name)
        {
            throw new NotImplementedException();
        }

        public Task<ICharacter> GetCharacterForUser(IUser user)
        {
            throw new NotImplementedException();
        }

        public Task SaveCharacter(ICharacter character)
        {
            throw new NotImplementedException();
        }
    }
}
