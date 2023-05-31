namespace Management.Application
{
    public class RoleDto:EntityDto<long>
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; } = string.Empty;
        /// <summary>
        /// 是否禁用
        /// </summary>
        public bool IsDisabled { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// 权限Code数组
        /// </summary>
        public IReadOnlyCollection<string> PermissionCodes { get; set; } = new List<string>();
    }
}
