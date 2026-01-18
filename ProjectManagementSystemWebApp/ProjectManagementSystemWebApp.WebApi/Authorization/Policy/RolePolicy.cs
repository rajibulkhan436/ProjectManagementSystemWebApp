using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace ProjectManagementSystemWebApp.WebApi.Authorization.Policy
{
    public class RolePolicy
    {
        public class Requirements : IAuthorizationRequirement
        {
            public required List<string> roles { get; set; }
        }

        public class Handler : AuthorizationHandler<Requirements>
        {
            protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, Requirements requirement)
            {
                if (context.User.Identity?.IsAuthenticated != true)
                {
                    return Task.CompletedTask;
                }
                string? userRole = context.User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Role)?.Value;
                if (!string.IsNullOrEmpty(userRole) && requirement.roles.Contains(userRole))
                {
                    context.Succeed(requirement);
                }
                return Task.CompletedTask;
            }
        }

    }
}
