using Application.Abstractions.Behavior.Messaging;
using Domain.Abstractions;
using Domain.Books;
using Domain.Loans;
using Domain.Reader;

namespace Application.Readers.NewLoan;

public class NewLoanCommandHandler : ICommandHandler<NewLoanCommand, Guid>
{
    private readonly IBookRepository _bookRepository;
    private readonly IReaderRepository _readerRepository;
    private readonly ILoanRepository _loanRepository;
    private readonly IUnitOfWork _unitOfWork;

    public NewLoanCommandHandler(IBookRepository bookRepository, IReaderRepository readerRepository, ILoanRepository loanRepository, IUnitOfWork unitOfWork)
    {
        _bookRepository = bookRepository;
        _readerRepository = readerRepository;
        _loanRepository = loanRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(NewLoanCommand request, CancellationToken cancellationToken)
    {
        var reader = await _readerRepository.GetByIdAsync(request.ReaderId);

        if (reader is null)
        {
            return Result.Failure<Guid>(ReaderErrors.ReaderNotFound(request.ReaderId));
        }

        var book = await _bookRepository.GetByIdAsync(request.BookId);

        if (book is null)
        {
            return Result.Failure<Guid>(BookErrors.BookNotFound(request.BookId));
        }

        var loan = reader.NewLoan(book, request.NDays);

        if (loan.IsFailure)
        {
            return Result.Failure<Guid>(loan.Error);
        }

        _loanRepository.Add(loan.Value);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return loan.Value.Id;
    }
}
