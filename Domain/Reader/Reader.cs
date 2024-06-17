using Domain.Abstractions;
using Domain.Books;
using Domain.Loans;
using Domain.Reader.Events;

namespace Domain.Reader;

public class Reader : Entity
{
    private Reader() { }
    private Reader(
        Guid id,
        FullName? fullName,
        Email? email)
        : base(id)
    {
        FullName = fullName;
        Email = email;
    }

    public FullName? FullName { get; private set; }
    public Email? Email { get; private set; }
    private readonly List<Loan> _loans = new();
    public List<Loan> Loans => _loans;
    private readonly List<Role> _roles = new();
    public List<Role> Roles => _roles.ToList();

    public static Result<Reader> Create(FullName? fullName, Email? email)
    {
        var reader = new Reader(Guid.NewGuid(), fullName, email);

        reader.Raise(new ReaderCreatedDomainEvent(reader.Id));

        return reader;
    }

    public Result<Loan> NewLoan(Book book, int days)
    {
        if (days <= 0 || days > 5)
        {
            return Result.Failure<Loan>(ReaderErrors.LoanDaysOutOfRange());
        }

        var result = book.Borrow();

        if (result.IsFailure)
        {
            return Result.Failure<Loan>(result.Error);
        }

        var loan = new Loan(Guid.NewGuid(), Id, book.Id, DateTime.UtcNow, DateTime.UtcNow.AddDays(days));

        _loans.Add(loan);

        Raise(new LoanConfirmedDomainEvent(Id));

        return loan;
    }

    public void AddRole(Role role)
    {
        _roles.Add(role);
    }
}
