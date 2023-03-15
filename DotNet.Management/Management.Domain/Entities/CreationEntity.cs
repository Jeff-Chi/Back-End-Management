using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Domain
{
    public class CreationEntity<TKey> : Entity<TKey>
    {
        public CreationEntity(TKey id) : base(id)
        {
        }

        /// <summary>
        /// 创建人ID
        /// </summary>
        public long? CreatorId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; } = DateTime.Now;
    }
}
