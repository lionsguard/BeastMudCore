using Beast.Security;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Beast.Sqlite.Security
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        public UserRepository(SqliteContext context)
            : base(context)
        {
        }

        public async Task<IUser> GetUserByEmail(string email)
        {
            return await Context.Users.FirstOrDefaultAsync(o => o.Email == email);
        }

        public async Task<IUser> GetUserById(Guid id)
        {
            return await Context.Users.FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<IUser> GetUserByName(string name)
        {
            return await Context.Users.FirstOrDefaultAsync(o => o.Name == name);
        }

        public async Task<bool> IsNameAvailable(string name)
        {
            var dt = DateTime.UtcNow;

            // Delete all inactive users created more than 4 hours ago.
            var inactives = await Context.Users.Where(o => !o.IsActive && (dt - o.CreatedDate).Hours > 4).ToListAsync();
            inactives.ForEach(o => Context.Entry(o).State = EntityState.Deleted);

            await Context.SaveChangesAsync();

            return !(await Context.Users.AnyAsync(o => o.Name == name));
        }

        public async Task SaveUser(IUser user)
        {
            var usr = user as User;
            if (usr == null)
                return;

            if (await Context.Users.AnyAsync(o => o.Id == user.Id))
            {
                Context.Users.Update(usr);
            }
            else
            {
                await Context.Users.AddAsync(usr);
            }

            await Context.SaveChangesAsync();
        }
    }
}
