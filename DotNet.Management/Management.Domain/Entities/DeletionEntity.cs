using Management.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Domain
{
    public class DeletionEntity<TKey> : ModificationEntity<TKey>, IFullAuditedObject
    {
        public DeletionEntity(TKey id) : base(id)
        {
        }

        /// <summary>
        /// 删除人Id
        /// </summary>
        public long? DeleterId { get; set; }

        /// <summary>
        /// 删除时间
        /// </summary>
        public DateTime? DeletionTime { get; set; }

        /// <summary>
        /// 是否已删除
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}
