using Application.Abstractions.Behavior.Messaging;
using Application.Abstractions.Data;
using Domain.Abstractions;
using Domain.Books;
using Microsoft.EntityFrameworkCore;

namespace Application.Books.GetByTitle;

public class GetBookByTitleQueryHandler : IQueryHandler<GetBookByTitleQuery, GetBookByTitleResponse>
{
    private readonly IApplicationDbContext _context;

    public GetBookByTitleQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<GetBookByTitleResponse>> Handle(GetBookByTitleQuery request, CancellationToken cancellationToken)
    {
        var book = await _context.Books
            .AsNoTracking()
            .Where(b => b.Title == new Title(request.Title))
            .Select(b => new GetBookByTitleResponse(
                b.Id,
                b.Title!.Value,
                b.Author.Name,
                b.Author.LastName,
                b.PublishDate,
                b.Status,
                b.CategoryId
                ))
            .FirstOrDefaultAsync(cancellationToken);

        if (book is null)
        {
            return Result.Failure<GetBookByTitleResponse>(BookErrors.BookTitleNotFound(request.Title));
        }

        return book;
    }
}
