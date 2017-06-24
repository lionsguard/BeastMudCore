using Beast.Security;
using System;
using System.Threading.Tasks;

namespace Beast.Characters
{
    public interface ICharacterRepository : IDisposable
    {
        Task<ICharacter> GetCharacterByName(string name);
        Task<ICharacter> GetCharacterById(Guid id);

        Task<ICharacter> GetCharacterForUser(IUser user);
        
        Task SaveCharacter(ICharacter character);
    }
}
