using Application.Abstractions.Behavior.Messaging;

namespace Application.Readers.Return;

public sealed record ReturnBookCommand(Guid BookId) : ICommand<ReturnBookResponse>;

public sealed record ReturnBookRequest(Guid BookId);

public sealed record ReturnBookResponse(string Message);