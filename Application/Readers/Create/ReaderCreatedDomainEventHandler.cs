using Domain.Reader;
using MediatR;

namespace Application.Readers.Create;

public class ReaderCreatedDomainEventHandler : INotificationHandler<ReaderCreatedDomainEvent>
{
    public Task Handle(ReaderCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
