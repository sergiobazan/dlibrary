using Application.Abstractions.Behavior.Messaging;

namespace Application.Books.Create;

public sealed record CreateBookCommand(
    string Title,
    DateTime PublishDate,
    string AuthorName,
    string AuthorLastName,
    DateTime AuthorDoB,
    Guid CategoryId) : ICommand<Guid>;


public sealed record CreateBookResponse(
    string Title,
    DateTime PublishDate,
    string AuthorName,
    string AuthorLastName,
    DateTime AuthorDoB);