namespace Domain.Reader;

public interface IReaderRepository
{
    void Add(Reader reader);

    Task<Reader?> GetByIdAsync(Guid id);
}
