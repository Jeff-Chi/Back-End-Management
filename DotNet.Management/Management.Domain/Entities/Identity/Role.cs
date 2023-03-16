namespace Management.Domain
{
    public class Role : ModificationEntity<long>
    {
        public Role(long id) : base(id)
        {
        }

        public string Name { get; set; } = string.Empty;

        public string Code { get; set; } = string.Empty;
    }
}
