namespace Domain.Abstractions;

public abstract class Entity
{
    public Guid Id { get; init; }

    private List<IDomainEvent> _domainEvents = new();

    protected Entity(Guid id)
    {
        Id = id;
    }

    protected Entity() { }

    public List<IDomainEvent> DomainEvents => _domainEvents.ToList();

    public void Raise(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}
