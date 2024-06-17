namespace Application.Abstractions.Behavior;

public interface IPermissionService
{
    Task<HashSet<string>> GetPermissionAsync(Guid readerId);
}
