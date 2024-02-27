namespace Domain.Books;

public interface IBookRepository
{
    void Add(Book book);
    Task<Book?> GetByIdAsync(Guid id);

    Task<bool> CategoryExistsAsync(Guid categoryId);
}
