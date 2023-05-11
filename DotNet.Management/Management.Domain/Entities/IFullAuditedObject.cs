using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Domain.Entities
{
    public interface IFullAuditedObject
    {
        public bool IsDeleted { get; set; }
        public long? DeleterId { get; set; }
        public DateTime? DeletionTime { get; set; }
    }
}
