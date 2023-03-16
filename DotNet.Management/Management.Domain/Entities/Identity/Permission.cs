using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Domain
{
    public class Permission
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string? ParentCode { get; set; }
        public int SortOrder { get; set; }
    }
}
