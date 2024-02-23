using Domain.Abstractions;

namespace Domain.Books.Events;

public sealed record BookCreatedDomainEvent(Guid Id) : IDomainEvent;