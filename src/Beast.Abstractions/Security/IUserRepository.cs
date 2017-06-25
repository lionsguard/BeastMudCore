using System;
using System.Threading.Tasks;

namespace Beast.Security
{
    public interface IUserRepository : IDisposable
    {
        Task<bool> IsNameAvailable(string name);

        Task<IUser> GetUserByName(string name);
        Task<IUser> GetUserByEmail(string email);
        Task<IUser> GetUserById(Guid id);

        Task SaveUser(IUser user);
    }
}
