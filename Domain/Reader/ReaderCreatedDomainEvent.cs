using Domain.Abstractions;

namespace Domain.Reader;

public sealed record ReaderCreatedDomainEvent(Guid ReaderId) : IDomainEvent;