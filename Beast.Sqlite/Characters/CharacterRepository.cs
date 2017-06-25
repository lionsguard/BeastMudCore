using Beast.Characters;
using Beast.Security;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Beast.Sqlite.Characters
{
    public class CharacterRepository : RepositoryBase, ICharacterRepository
    {
        public CharacterRepository(SqliteContext context) 
            : base(context)
        {
        }

        public async Task<ICharacter> GetCharacterById(Guid id)
        {
            return await Context.Characters.FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<ICharacter> GetCharacterByName(string name)
        {
            return await Context.Characters.FirstOrDefaultAsync(o => o.Name == name);
        }

        public async Task<ICharacter> GetCharacterForUser(IUser user)
        {
            return await Context.Characters.FirstOrDefaultAsync(o => o.UserId == user.Id);
        }

        public async Task SaveCharacter(ICharacter character)
        {
            var c = character as Character;
            if (c == null)
                return;

            if (await Context.Characters.AnyAsync(o => o.Id == character.Id))
            {
                Context.Update(c);
            }
            else
            {
                await Context.AddAsync(c);
            }
            await Context.SaveChangesAsync();
        }
    }
}
