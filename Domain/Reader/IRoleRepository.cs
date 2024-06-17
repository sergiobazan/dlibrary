namespace Domain.Reader;

public interface IRoleRepository
{
    Task<Role> CreateAsync(string name);
    Task<Role?> GetRoleAsync(string name);
}
