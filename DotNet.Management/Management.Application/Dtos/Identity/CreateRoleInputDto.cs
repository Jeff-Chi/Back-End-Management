using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Application
{
    public class CreateRoleInputDto
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; } = string.Empty;
        /// <summary>
        /// 描述
        /// </summary>
        public string? Description { get; set; }
    }
}
