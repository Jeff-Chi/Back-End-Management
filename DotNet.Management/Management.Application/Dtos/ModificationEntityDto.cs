namespace Management.Application
{
    public class ModificationEntityDto<TKey> : CreationEntityDto<TKey>  where TKey : struct
    {
        /// <summary>
        /// 修改人ID
        /// </summary>
        public long? ModifierId { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? ModificationTime { get; set; }
    }
}
