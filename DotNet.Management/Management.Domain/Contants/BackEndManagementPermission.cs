using System.ComponentModel;

namespace Management.Domain
{
    public static class BackEndManagementPermission
    {
        public const string BackEndManagement = "BackEndManagement";

        public static class UserPermissionGroup
        {
            [Description("用户管理")]
            public const string GroupName = BackEndManagement + ".User";
            [Description("查询")]
            public const string Query = GroupName + ".Query";
            [Description("创建")]
            public const string Create = GroupName + ".Create";
            [Description("更新")]
            public const string Update = GroupName + ".Update";
            [Description("删除")]
            public const string Delete = GroupName + ".Delete";
            [Description("设置角色")]
            public const string SetRole = GroupName + ".SetRole";
        }

        public static class RolePermissionGroup
        {
            [Description("角色管理")]
            public const string GroupName = BackEndManagement + ".Role";
            [Description("查询")]
            public const string Query = GroupName + ".Query";
            [Description("创建")]
            public const string Create = GroupName + ".Create";
            [Description("更新")]
            public const string Update = GroupName + ".Update";
            [Description("删除")]
            public const string Delete = GroupName + ".Delete";
            [Description("设置权限")]
            public const string SetPermission = GroupName + ".SetPermission";
        }

    }
}
