using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Domain
{
    public interface IUserRepository : IRepository<long, User>
    {
        Task<List<User>> GetListAsync(QueryUsersDto input);

        Task<int> CountAsync(QueryUsersDto input);
    }
}
