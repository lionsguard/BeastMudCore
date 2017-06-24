using System;
using System.Threading.Tasks;

namespace Beast.Security
{
    public interface IUserRepository : IDisposable
    {
        Task<IUser> GetUserByName(string name);
        Task<IUser> GetUserByEmail(string email);
        Task<IUser> GetUserById(Guid id);

        Task<ReservedName> ReserveName(string name);
        Task<bool> IsValidReservedName(ReservedName name);

        Task SaveUser(IUser user);
    }
}
