using Domain.Reader;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ReaderRepository : IReaderRepository
{
    private readonly ApplicationDbContext _context;

    public ReaderRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public void Add(Reader reader)
    {
        _context.Add(reader);
    }

    public async Task<Reader?> GetByIdAsync(Guid id)
    {
       return await _context.Set<Reader>().FirstOrDefaultAsync(x => x.Id == id);
    }
}
