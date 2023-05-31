using System.ComponentModel;

namespace Management.Domain
{
    public sealed class Permissions
    {
        public const string BackEndManagement = "BackEndManagement";

        public static class UserManagement
        {
            [Description("用户管理")]
            public const string GroupName = BackEndManagement + ".UserManagement";
            [Description("查询")]
            public const string Query = GroupName + ".Query";
            [Description("创建")]
            public const string Create = GroupName + ".Create";
            [Description("更新")]
            public const string Update = GroupName + ".Update";
            [Description("删除")]
            public const string Delete = GroupName + ".Delete";
            [Description("设置用户角色")]
            public const string ChangeUserRole = GroupName + ".ChangeUserRole";
        }

        public static class RoleManagement
        {
            [Description("角色管理")]
            public const string GroupName = BackEndManagement + ".RoleManagement";
            [Description("查询")]
            public const string Query = GroupName + ".Query";
            [Description("创建")]
            public const string Create = GroupName + ".Create";
            [Description("更新")]
            public const string Update = GroupName + ".Update";
            [Description("删除")]
            public const string Delete = GroupName + ".Delete";
            [Description("设置角色权限")]
            public const string ChangeRolePermission = GroupName + ".ChangeRolePermission";
        }

    }
}
