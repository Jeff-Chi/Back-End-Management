using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Domain.Entities.Identity
{
    public class RolePermission
    {
        public RolePermission(long roleId, string permissionCode)
        {
            RoleId = roleId;
            PermissionCode = permissionCode;
        }

        public long RoleId { get; set; }
        public string PermissionCode { get; set; } = string.Empty;
    }
}
