using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Domain
{
    public class SortingDto: PageDto
    {
        /// <summary>
        /// 排序
        /// </summary>
        public string? Sorting { get; set; }
    }
}
