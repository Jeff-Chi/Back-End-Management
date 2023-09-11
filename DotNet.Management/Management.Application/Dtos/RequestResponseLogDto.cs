namespace Management.Application
{
    public class RequestResponseLogDto
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string? UserName { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string? NickName { get; set; }
        public long? UserId { get; set; }
        public string? Brower { get; set; }
        public string? Platform { get; set; }
        public string? Controller { get; set; }
        public string? Action { get; set; }

        public string RequestPath { get; set; } = string.Empty;
        public string? RequestQuery { get; set; }
        //public List<KeyValuePair<string, string>> RequestQueries { get; set; }
        public string RequestMethod { get; set; } = string.Empty;
        public string? RequestScheme { get; set; }
        public string? RequestHost { get; set; }
        //public Dictionary<string, string> RequestHeaders { get; set; }
        public string? RequestBody { get; set; }
        public string? RequestContentType { get; set; }
        public string SourceIpAddress { get; set; } = string.Empty;

        public string ResponseStatus { get; set; } = string.Empty;
        // public Dictionary<string, string> ResponseHeaders { get; set; }
        public string? ResponseBody { get; set; }
        public string? ResponseContentType { get; set; }

        public bool IsException { get; set; }
        public string? ExceptionMessage { get; set; }
        public string? ExceptionStackTrace { get; set; }
        public DateTime ResponseDateTime { get; set; }
        public DateTime RequestDateTime { get; set; }
    }
}
