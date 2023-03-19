using System.ComponentModel.DataAnnotations;

namespace Management.Domain
{
    public class Entity<TKey>
    {
        public Entity(TKey id)
        {
            Id = id;
        }
        [Key]
        public TKey Id { get; set; }
    }
}
