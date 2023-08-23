namespace Management.Domain
{
    public class AuditLog : CreationEntity<long>
    {
        public AuditLog(long id) : base(id)
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
        public string Url { get; set; } = string.Empty;
        public string HttpMethod { get; set; } = string.Empty;
        public string SourceIpAddress { get; set; } = string.Empty;
        public string? Brower { get; set; }
        public string? Platform { get; set; }
        public string Controller { get; set; } = string.Empty;
        public string Action { get; set; } = string.Empty;
        public string? Body { get; set; }
        public string? QueryString { get; set; }
        public string StatuCode { get; set; } = string.Empty;
        public bool IsSucceed { get; set; }
    }
}
