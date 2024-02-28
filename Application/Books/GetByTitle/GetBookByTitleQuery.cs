using Application.Abstractions.Behavior.Messaging;
using Domain.Books;

namespace Application.Books.GetByTitle;

public sealed record GetBookByTitleQuery(string Title) : IQuery<GetBookByTitleResponse>;

public sealed record GetBookByTitleResponse(
    Guid Id,
    string Title,
    string AuthorName,
    string AuthorLastName,
    DateTime PublishDate,
    Status Status,
    Guid CategoryId);