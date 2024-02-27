using Domain.Abstractions;

namespace Domain.Books.Events;

public sealed record BookReturnedDomainEvent(Guid Id) : IDomainEvent;