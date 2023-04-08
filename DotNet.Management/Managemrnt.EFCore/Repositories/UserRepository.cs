using Management.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Managemrnt.EFCore
{
    public class UserRepository: Repository<long, User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<User>> GetListAsync(QueryUsersDto input)
        {
             return await Build(DbSet,input,true).ToListAsync();
        }

        public async Task<int> CountAsync(QueryUsersDto input)
        {
            return await Build(DbSet, input).CountAsync();
        }

        private IQueryable<User> Build(IQueryable<User> query, QueryUsersDto? input, bool page = false)
        {
            if (input == null)
            {
                return query;
            }

            // TODO: 拼接查询条件

            return query;
        }
    }
}
