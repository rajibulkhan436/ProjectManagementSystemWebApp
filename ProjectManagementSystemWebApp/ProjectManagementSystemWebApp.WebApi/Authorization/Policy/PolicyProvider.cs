using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using ProjectManagementSystemWebApp.WebApi.Constants;

namespace ProjectManagementSystemWebApp.WebApi.Authorization.Policy
{
    public class PolicyProvider : IAuthorizationPolicyProvider
    {
        private readonly DefaultAuthorizationPolicyProvider _defaultPolicyProvider;

        public PolicyProvider(IOptions<AuthorizationOptions> options)
        {
            _defaultPolicyProvider = new DefaultAuthorizationPolicyProvider(options);
        }

        public Task<AuthorizationPolicy> GetDefaultPolicyAsync() =>
            _defaultPolicyProvider.GetDefaultPolicyAsync();

        public Task<AuthorizationPolicy?> GetFallbackPolicyAsync() =>
            Task.FromResult<AuthorizationPolicy?>(null);

        public Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
        {
            if (string.IsNullOrWhiteSpace(policyName))
                return Task.FromResult<AuthorizationPolicy?>(null);

            var policies = policyName.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            var policyBuilder = new AuthorizationPolicyBuilder();

            foreach (var policy in policies)
            {
                var roles = policy switch
                {
                    RolePolicyNames.EmployeePolicy => new List<string> { "Member", "Manager" },
                    RolePolicyNames.AdminPolicy => new List<string> { "Manager" },
                    _ => null
                };

                if (roles != null)
                {
                    policyBuilder.AddRequirements(new RolePolicy.Requirements { roles = roles });
                }
            }

            return Task.FromResult<AuthorizationPolicy?>(policyBuilder.Build());
        }
    }

}
