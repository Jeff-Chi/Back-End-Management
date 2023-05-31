using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Domain
{
    public interface IRoleRepository: IRepository<long, Role>
    {
        Task<int> CountAsync(GetRolesInput input);
        Task<List<Role>> GetListAsync(GetRolesInput input);
        Task<List<Role>> GetListAsync(long userId);
        Task<Role?> GetAsync(long id, GetRoleDetailsInput input);
        Task<List<Permission>> GetPermissionsAsync();
        Task<List<Permission>> GetPermissionsAsync(List<long> roleIds);

    }
}
