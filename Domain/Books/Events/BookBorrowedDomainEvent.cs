using Domain.Abstractions;

namespace Domain.Books.Events;

public sealed record BookBorrowedDomainEvent(Guid Id) : IDomainEvent;