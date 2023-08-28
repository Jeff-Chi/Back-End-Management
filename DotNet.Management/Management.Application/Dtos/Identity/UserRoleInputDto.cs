namespace Management.Application
{
    public class UserRoleInputDto
    {
        public long Id { get; set; }
        public List<long> RoleIds { get; set; } = new List<long>();
    }
}
