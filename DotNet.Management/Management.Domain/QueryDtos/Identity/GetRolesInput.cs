using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Domain
{
    public class GetRolesInput:SortingDto
    {
        public string? Name { get; set; }
        public bool? IsDisabled { get; set; }
        public string? Code { get; set; }
        public List<long>? Ids { get; set; }
        public bool IncludeRolePermission { get; set; }
    }
}
