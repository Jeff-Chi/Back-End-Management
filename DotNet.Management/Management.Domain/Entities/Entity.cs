namespace Management.Domain
{
    public class Entity<TKey>
    {
        public Entity(TKey id)
        {
            Id = id;
        }
        public TKey Id { get; set; }
    }
}
