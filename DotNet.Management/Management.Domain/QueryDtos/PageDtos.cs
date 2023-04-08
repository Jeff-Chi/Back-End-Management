namespace Management.Domain
{
    public class PageDtos
    {
        /// <summary>
        /// 跳过多少条
        /// </summary>
        public int SkipCount { get; set; }

        /// <summary>
        /// 取的数据条数
        /// </summary>
        public int MaxResultCount { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public string? Sorting { get; set; }
    }
}
