using Domain.Books.Events;
using MediatR;

namespace Application.Books.Create;

public class BookCreatedDomainEventHandler : INotificationHandler<BookCreatedDomainEvent>
{
    public Task Handle(BookCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
