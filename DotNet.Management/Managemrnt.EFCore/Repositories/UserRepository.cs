using Management.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Managemrnt.EFCore
{
    public class UserRepository: Repository<long, User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<User>> GetListAsync(GetUsersInput input)
        {
             return await Build(DbSet,input,true).ToListAsync();
        }

        public async Task<int> CountAsync(GetUsersInput input)
        {
            return await Build(DbSet, input).CountAsync();
        }

        public async Task<User?> GetAsync(string account, string password)
        {
            return await DbSet.Where(u => u.UserName == account && u.Password == password).FirstOrDefaultAsync();
        }

        public async Task<User?> GetAsync(long id, GetUserDetailsInput? input)
        {
            var query = DbSet.Where(u => u.Id == id);
            if (input != null)
            {
                query = query.IncludeIf(input.IncludeUserRoles, u => u.UserRoles);
            }
            return await query.FirstOrDefaultAsync();
        }

        #region private methods
        private IQueryable<User> Build(IQueryable<User> query, GetUsersInput? input, bool page = false)
        {
            if (input == null)
            {
                return query;
            }

            query = query.WhereIf(!string.IsNullOrWhiteSpace(input.UserName), u => u.UserName.Contains(input.UserName!))
                .WhereIf(input.IsDisabled.HasValue, u => u.IsDisabled == input.IsDisabled)
                .PageIf(page,input);

            return query;
        }
        #endregion

    }
}
