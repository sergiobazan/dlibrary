using Domain.Abstractions;

namespace Domain.Reader.Events;

public sealed record ReaderCreatedDomainEvent(Guid ReaderId) : IDomainEvent;