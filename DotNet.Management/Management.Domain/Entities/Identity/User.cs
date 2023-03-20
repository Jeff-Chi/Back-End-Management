using System.Collections;

namespace Management.Domain
{
    /// <summary>
    /// 用户
    /// </summary>
    public class User : ModificationEntity<long>
    {
        public User(long id) : base(id)
        {
        }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; } = string.Empty;
        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; } = string.Empty;
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; } = string.Empty;
        /// <summary>
        /// 头像 
        /// </summary>
        public string? Avatars { get; set; }
        /// <summary>
        /// 是否禁用
        /// </summary>
        public bool IsDisabled { get; set; }
        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime? ExpireDate { get; set; }
        /// <summary>
        /// 联系方式
        /// </summary>
        public string? MobilePhone { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string? Email { get; set; }
        /// <summary>
        /// 上次登录时间
        /// </summary>
        public DateTime? LastLoginTime { set; get; }

        #region navigation properties

        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();

        #endregion

    }
}
