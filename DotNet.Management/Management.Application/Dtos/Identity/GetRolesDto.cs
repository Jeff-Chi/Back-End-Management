using Management.Domain;

namespace Management.Application
{
    public class GetRolesDto:PageDto
    {
        public string? Name { get; set; }
        public bool? IsDisabled { get; set; }
    }
}
