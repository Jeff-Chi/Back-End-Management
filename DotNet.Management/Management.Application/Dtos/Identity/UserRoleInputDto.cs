using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Application
{
    public class UserRoleInputDto
    {
        public long Id { get; set; }
        public List<long> RoleIds { get; set; } = new List<long>();
    }
}
