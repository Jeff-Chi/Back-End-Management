using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Domain
{
    public interface IUserRepository : IRepository<long, User>
    {
        Task<List<User>> GetListAsync(GetUsersInput input);

        Task<int> CountAsync(GetUsersInput input);

        Task<User?> GetAsync(string account,string password);

        Task<User?> GetAsync(long id, GetUserDetailsInput? input);
    }
}
