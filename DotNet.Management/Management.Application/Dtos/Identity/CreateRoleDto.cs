namespace Management.Application
{
    public class CreateRoleDto: UpdateRoleDto
    {
        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; } = string.Empty;
    }

    public class UpdateRoleDto
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 描述
        /// </summary>
        public string? Description { get; set; }
    }
}
