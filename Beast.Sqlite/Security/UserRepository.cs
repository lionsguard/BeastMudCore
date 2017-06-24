using Beast.Security;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Beast.Sqlite.Security
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        public UserRepository(SqliteContext context)
            : base(context)
        {
        }

        public Task<IUser> GetUserByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public Task<IUser> GetUserById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IUser> GetUserByName(string name)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsValidReservedName(ReservedName name)
        {
            throw new NotImplementedException();
        }

        public Task<ReservedName> ReserveName(string name)
        {
            throw new NotImplementedException();
        }

        public Task SaveUser(IUser user)
        {
            throw new NotImplementedException();
        }
    }
}
