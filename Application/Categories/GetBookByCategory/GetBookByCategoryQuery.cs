using Application.Abstractions.Behavior.Messaging;
using Application.Books.Create;

namespace Application.Categories.GetBookByCategory;

public sealed record GetBookByCategoryQuery(Guid Id) : IQuery<GetBookByCategoryResponse>;


public sealed record GetBookByCategoryResponse(
    Guid Id,
    string Name,
    string Description,
    List<CreateBookResponse> Books);