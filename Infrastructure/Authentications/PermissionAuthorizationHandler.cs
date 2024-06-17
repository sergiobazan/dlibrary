using Microsoft.AspNetCore.Authorization;

namespace Infrastructure.Authentications;

internal class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context, 
        PermissionRequirement requirement)
    {
        HashSet<string> roles = context
            .User
            .Claims
            .Where(claim => claim.Type == CustomClaims.Permissions)
            .Select(claim => claim.Value)
            .ToHashSet();

        if (roles.Contains(requirement.Permission))
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}
