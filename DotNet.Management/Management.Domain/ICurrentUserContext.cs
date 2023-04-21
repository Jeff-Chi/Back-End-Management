using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Domain
{
    public interface ICurrentUserContext
    {
        long? Id { get; }
        string? UserName { get; }
        HashSet<string> PermissionCodes { get;}
        HashSet<long> RoleIds { get; }

        void SetValue(
            long? id,
            string? userName,
            HashSet<string> permissionCods,
            HashSet<long> roleIds);
    }
}
