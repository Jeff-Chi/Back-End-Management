namespace Management.Application
{
    public class CurrentUserDto
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; } = string.Empty;
        /// <summary>
        /// 用户昵称
        /// </summary>
        public string NickName { get; set; } = string.Empty;
        /// <summary>
        /// 权限列表
        /// </summary>
        public List<PermissionDto> Permissions { get; set; } = new List<PermissionDto>();
    }
}
