using Application.Abstractions.Behavior.Messaging;
using Domain.Abstractions;
using Domain.Books;

namespace Application.Readers.Return;

public class ReturnBookCommandHandler : ICommandHandler<ReturnBookCommand, ReturnBookResponse>
{
    private readonly IBookRepository _bookRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ReturnBookCommandHandler(IBookRepository bookRepository, IUnitOfWork unitOfWork)
    {
        _bookRepository = bookRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<ReturnBookResponse>> Handle(ReturnBookCommand request, CancellationToken cancellationToken)
    {
        var book = await _bookRepository.GetByIdAsync(request.BookId);

        if (book is null)
        {
            return Result.Failure<ReturnBookResponse>(BookErrors.BookNotFound(request.BookId));
        }

        var result = book.Return();

        if (result.IsFailure)
        {
            return Result.Failure<ReturnBookResponse>(result.Error);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new ReturnBookResponse("Book successfully return");
    }
}
