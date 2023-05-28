using Management.Domain;

namespace Management.Application
{
    public class GetRolesInputDto:PageDto
    {
        public string? Name { get; set; }
        public bool? IsDisabled { get; set; }
    }
}
