using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Filters;
using ProjectManagementSystemWebApp.WebApi.Authorization.Attributes;

namespace ProjectManagementSystemWebApp.WebApi.Authorization.Attributes
{
    public class CustomAuthorizationAttribute : Attribute, IAuthorizationFilter
    {
        private readonly List<string> _roles;

        public CustomAuthorizationAttribute(string roles)
        {

            _roles = roles.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            ClaimsPrincipal user = context.HttpContext.User;

            if (user.Identity is null || !user.Identity.IsAuthenticated)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            string? userRole = user.FindFirst(ClaimTypes.Role)?.Value;
            if (userRole is null || !_roles.Contains(userRole, StringComparer.OrdinalIgnoreCase))
            {
                context.Result = new ForbidResult();
            }
        }
    }
}