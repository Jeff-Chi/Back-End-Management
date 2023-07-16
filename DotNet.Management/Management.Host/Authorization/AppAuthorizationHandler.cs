using Management.Domain;
using Microsoft.AspNetCore.Authorization;

namespace Management.Host
{
    public class AppAuthorizationHandler : AuthorizationHandler<AppAuthorizationRequirement>
    {
        //private readonly ICurrentUserContext _currentUserContext;
        public AppAuthorizationHandler(ICurrentUserContext currentUserContext)
        {
            //_currentUserContext = currentUserContext;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AppAuthorizationRequirement requirement)
        {
            if (requirement.PermissionCode == null)
            {
                context.Succeed(requirement);
            }
            else
            {
                //if (_currentUserContext.PermissionCodes.Any())
                //{
                //    if (_currentUserContext.PermissionCodes.Contains(requirement.PermissionCode))
                //    {
                //        context.Succeed(requirement);
                //    }
                //}
                //var permissions = requirement.PermissionCode.Split('|', StringSplitOptions.RemoveEmptyEntries);
                //foreach (var permission in permissions)
                //{
                //    if (_currentUserContext.PermissionCodes.Contains(permission.Trim()))
                //    {
                //        context.Succeed(requirement);
                //        break;
                //    }
                //}
            }

            return Task.CompletedTask;
        }
    }
}
