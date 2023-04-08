namespace Management.Application
{
    public class EntityDto<TKey> where TKey:struct
    {
        public TKey Id { get; set; } = default;
    }
}
