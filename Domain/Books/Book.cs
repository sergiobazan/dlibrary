using Domain.Abstractions;
using Domain.Books.Events;

namespace Domain.Books;

public class Book : Entity
{
    private Book() { }
    private Book(
        Guid id,
        Title? title,
        Author author,
        DateTime publishDate,
        Status status,
        Guid categoryId)
        : base(id)
    {
        Title = title;
        Author = author;
        PublishDate = publishDate;
        Status = status;
        CategoryId = categoryId;
    }

    public Title? Title { get; private set; }
    public Author Author { get; private set; }
    public DateTime PublishDate { get; private set; }
    public Status Status { get; private set; }
    public Guid CategoryId { get; private set; }

    public static Result<Book> Create(
        Title? title,
        Author author,
        DateTime publishDate,
        Guid categoryId)
    {
        var book = new Book(Guid.NewGuid(), title, author, publishDate, Status.Available, categoryId);

        book.Raise(new BookCreatedDomainEvent(book.Id));

        return book;
    }

    public Result Borrow()
    {
        if (this.Status != Status.Available)
        {
            return BookErrors.BookIsNotAvailable(Id);
        }

        this.Status = Status.Borrowed;

        Raise(new BookBorrowedDomainEvent(Id));

        return Result.Success();
    }

    public Result Return()
    {
        if (this.Status != Status.Borrowed)
        {
            return BookErrors.BookIsNotBorrow(Id);
        }

        this.Status = Status.Available;

        Raise(new BookReturnedDomainEvent(Id));

        return Result.Success();
    }
}

