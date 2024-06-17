using Application.Abstractions.Behavior;
using Domain.Reader;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Authentications;

public class PermissionService(ApplicationDbContext context) : IPermissionService
{
    public async Task<HashSet<string>> GetPermissionAsync(Guid readerId)
    {
        List<Role>[] roles = await context
            .Readers
            .Include(reader => reader.Roles)
            .Where(reader => reader.Id == readerId)
            .Select(reader => reader.Roles)
            .ToArrayAsync();

        return roles
            .SelectMany(roles => roles)
            .Select(role => role.Name)
            .ToHashSet();
    }
}
