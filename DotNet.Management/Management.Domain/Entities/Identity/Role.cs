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
        /// 描述
        /// </summary>
        public string Description { get; set; }

        #region navigation properties

        public ICollection<RolePermission> RolePermissions { get; set; }

        #endregion
    }
}
