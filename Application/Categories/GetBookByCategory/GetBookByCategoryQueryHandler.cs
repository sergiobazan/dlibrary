using Application.Abstractions.Behavior.Messaging;
using Application.Abstractions.Data;
using Application.Books.Create;
using Domain.Abstractions;
using Domain.Categories;
using Microsoft.EntityFrameworkCore;

namespace Application.Categories.GetBookByCategory;

public class GetBookByCategoryQueryHandler : IQueryHandler<GetBookByCategoryQuery, GetBookByCategoryResponse>
{
    private readonly IApplicationDbContext _context;

    public GetBookByCategoryQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<GetBookByCategoryResponse>> Handle(
        GetBookByCategoryQuery request, 
        CancellationToken cancellationToken)
    {
        var category = await _context.Categories
            .AsNoTracking()
            .Where(c => c.Id == request.Id)
            .Select(c => new GetBookByCategoryResponse(
                c.Id,
                c.Name.Value,
                c.Description.Value,
                c.Books.Select(b => new CreateBookResponse(
                    b.Title.Value,
                    b.PublishDate,
                    b.Author.Name,
                    b.Author.LastName,
                    b.Author.DateOfBirth)).ToList()))
            .FirstOrDefaultAsync(cancellationToken);

        if (category is null)
        {
            return Result.Failure<GetBookByCategoryResponse>(CategoryErrors.CategoryNotFound(request.Id));
        }

        return category;
    }
}
