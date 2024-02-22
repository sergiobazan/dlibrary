using Application.Abstractions.Data;
using Domain.Reader;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Readers.Get;

public class GetReaderQueryHandler : IRequestHandler<GetReaderQuery, ReaderResponse>
{
    private readonly IApplicationDbContext _context;

    public GetReaderQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ReaderResponse> Handle(GetReaderQuery request, CancellationToken cancellationToken)
    {
        var reader = await _context.Readers
            .Where(r => r.Id == request.Id)
            .Select(r => new ReaderResponse(
                r.Id,
                r.FullName!.Name,
                r.FullName.LastName,
                r.Email!.Value))
            .FirstOrDefaultAsync();

        if (reader is null)
        {
            throw new ReaderException();
        }

        return reader;
    }
}
