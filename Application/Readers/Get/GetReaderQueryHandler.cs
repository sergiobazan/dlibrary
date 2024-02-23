using Application.Abstractions.Behavior.Messaging;
using Application.Abstractions.Data;
using Domain.Abstractions;
using Domain.Reader;
using Microsoft.EntityFrameworkCore;

namespace Application.Readers.Get;

public class GetReaderQueryHandler : IQueryHandler<GetReaderQuery, ReaderResponse>
{
    private readonly IApplicationDbContext _context;

    public GetReaderQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<ReaderResponse>> Handle(GetReaderQuery request, CancellationToken cancellationToken)
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
           return Result.Failure<ReaderResponse>(ReaderErrors.ReaderNotFound(request.Id));
        }

        return reader;
    }
}
