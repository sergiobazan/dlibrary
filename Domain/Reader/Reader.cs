using Domain.Abstractions;

namespace Domain.Reader;

public class Reader : Entity
{
    public FullName? FullName { get; private set; }
    public Email? Email { get; private set; }

    private Reader(
        Guid id,
        FullName? fullName,
        Email? email) 
        : base(id)
    {
        FullName = fullName;
        Email = email;
    }

    public Reader() { }

    public static Result<Reader> Create(FullName? fullName, Email? email)
    {
        var reader = new Reader(Guid.NewGuid(), fullName, email);

        reader.Raise(new ReaderCreatedDomainEvent(reader.Id));

        return reader;
    }
}
