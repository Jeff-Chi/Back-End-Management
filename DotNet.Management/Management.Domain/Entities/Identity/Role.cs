namespace Management.Domain
{
    /// <summary>
    /// 角色
    /// </summary>
    public class Role : ModificationEntity<long>
    {
        public Role(long id) : base(id)
        {
        }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; } = string.Empty;
        /// <summary>
        /// 是否超级管理员
        /// </summary>
        public bool IsSuperAdmin { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// 是否禁用
        /// </summary>
        public bool IsDisabled { get; set; }

        #region navigation properties

        public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();

        #endregion
    }
}
