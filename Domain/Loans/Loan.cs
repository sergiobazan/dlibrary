using Domain.Abstractions;

namespace Domain.Loans;

public class Loan : Entity
{
    internal Loan(
        Guid id,
        Guid readerId,
        Guid bookId,
        DateTime borrowDate,
        DateTime returnDate)
        : base(id)
    {
        ReaderId = readerId;
        BookId = bookId;
        BorrowDate = borrowDate;
        ReturnDate = returnDate;
    }

    public Guid ReaderId { get; private set; }
    public Guid BookId { get; private set; }
    public DateTime BorrowDate { get; private set; }
    public DateTime ReturnDate { get; private set; }

}
