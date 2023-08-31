namespace Management.Domain
{
    public class PageDto
    {
        /// <summary>
        /// 跳过多少条
        /// </summary>
        public int SkipCount { get; set; }

        /// <summary>
        /// 取的数据条数
        /// </summary>
        public int MaxResultCount { get; set; } = 10;

        
    }
}
