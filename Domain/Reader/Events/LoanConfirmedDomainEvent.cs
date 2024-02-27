using Domain.Abstractions;

namespace Domain.Reader.Events;

public sealed record LoanConfirmedDomainEvent(Guid Id) : IDomainEvent;
