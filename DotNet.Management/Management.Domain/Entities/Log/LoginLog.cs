namespace Management.Domain
{
    public class LoginLog : CreationEntity<long>
    {
        public LoginLog(long id) : base(id)
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
        public long? UserId { get; set; }
        /// <summary>
        /// IP地址
        /// </summary>
        public string SourceIpAddress { get; set; } = string.Empty;
        public string? Platform { get; set; }
        public string? Brower { get; set; }
        public long IsSucceed { get; set; }
    }
}
