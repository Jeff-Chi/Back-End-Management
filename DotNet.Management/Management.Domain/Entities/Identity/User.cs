namespace Management.Domain
{
    public class User : ModificationEntity<long>
    {
        public User(long id) : base(id)
        {
        }

        public string UserName { get; set; } = string.Empty;

        public string NickName { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string? Avatars { get; set; }

        public bool IsEnabled { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime? ExpireDate { get; set; }

        public string? MobilePhone { get; set; }

        public string? Email { get; set; }

        public DateTime LastLoginTime { set; get; }

        #region navigation properties



        #endregion

    }
}
