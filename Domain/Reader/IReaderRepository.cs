namespace Domain.Reader;

public interface IReaderRepository
{
    void Add(Reader reader);
    Task<Reader?> GetByEmailAsync(Email email);
    Task<Reader?> GetByIdAsync(Guid id);
}
