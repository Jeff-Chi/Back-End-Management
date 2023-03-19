namespace Management.Domain
{
    /// <summary>
    /// 用户角色关系
    /// </summary>
    public class UserRole
    {
        public UserRole(long userId,long roleId)
        {
            UserId = userId;
            RoleId = roleId;
        }

        public long UserId { get; set; }
        public long RoleId { get; set; }

        #region navigation properties

        public User? User { get; set; }
        public Role? Role { get; set; }

        #endregion
    }
}
