using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Domain
{
    /// <summary>
    /// 当前登录用户
    /// </summary>
    public class CurrentUserContext: ICurrentUserContext
    {
        public long? Id { get; private set; }

        public string? UserName { get; private set; }

        public HashSet<string> PermissionCodes { get; private set; } = new HashSet<string>();

        public HashSet<long> RoleIds { get; private set; } = new HashSet<long>();

        public void SetValue(
            long? id, 
            string? userName, 
            HashSet<string> permissionCods, 
            HashSet<long> roleIds)
        {
            Id = id;
            UserName = userName;
            PermissionCodes = permissionCods;
            RoleIds = roleIds;
        }
    }
}
