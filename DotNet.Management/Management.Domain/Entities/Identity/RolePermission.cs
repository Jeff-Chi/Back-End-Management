using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Domain
{
    /// <summary>
    /// 角色权限关系
    /// </summary>
    public class RolePermission
    {
        public RolePermission(long roleId, string permissionCode)
        {
            RoleId = roleId;
            PermissionCode = permissionCode;
        }

        /// <summary>
        /// 角色Id
        /// </summary>
        public long RoleId { get; set; }
        /// <summary>
        /// 权限编码
        /// </summary>
        public string PermissionCode { get; set; } = string.Empty;
    }
}
