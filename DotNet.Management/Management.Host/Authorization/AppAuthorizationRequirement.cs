using Microsoft.AspNetCore.Authorization;

namespace Management.Host
{
    public class AppAuthorizationRequirement:IAuthorizationRequirement
    {
        public string? PermissionCode { get; set; }
    }
}
