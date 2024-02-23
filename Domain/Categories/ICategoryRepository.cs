namespace Domain.Categories;

public interface ICategoryRepository
{
    void Add(Category category);

    Task<Category?> FindByIdAsync(Guid id);
}
