using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace Management.Host
{
    public class AppAuthorizationPolicyProvider : IAuthorizationPolicyProvider
    {
        private readonly AuthorizationOptions _authorizationOptions;
        private DefaultAuthorizationPolicyProvider DefaultPolicyProvider { get; }
        public AppAuthorizationPolicyProvider(IOptions<AuthorizationOptions> options)
        {
            DefaultPolicyProvider = new DefaultAuthorizationPolicyProvider(options);
            _authorizationOptions = options.Value;
        }


        public Task<AuthorizationPolicy> GetDefaultPolicyAsync()
        {
            return DefaultPolicyProvider.GetDefaultPolicyAsync();
        }

        public Task<AuthorizationPolicy?> GetFallbackPolicyAsync()
        {
            return DefaultPolicyProvider.GetFallbackPolicyAsync();
        }

        public async Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
        {
            if (!string.IsNullOrEmpty(policyName))
            {
                var policy = await DefaultPolicyProvider.GetPolicyAsync(policyName);
                if (policy is not null)
                {
                    return policy;
                }

                var builder = new AuthorizationPolicyBuilder();
                // 添加授权要求
                builder.AddRequirements(new AppAuthorizationRequirement { PermissionCode = policyName.Trim() });

                policy = builder.Build();
                _authorizationOptions.AddPolicy(policyName, policy);
                return policy;
            }

            return await DefaultPolicyProvider.GetPolicyAsync(policyName);
        }
    }
}
