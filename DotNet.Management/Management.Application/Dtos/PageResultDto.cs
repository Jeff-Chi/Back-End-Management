using System.Collections.Generic;

namespace Management.Application
{
    public class PageResultDto<T>
    {
        /// <summary>
        /// 总数
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// 列表
        /// </summary>
        public IReadOnlyList<T> Items { get; set; } = new List<T>();
    }
}
