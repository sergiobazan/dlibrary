using Application.Abstractions.Behavior.Messaging;
using Domain.Abstractions;
using Domain.Books;
using Domain.Categories;

namespace Application.Books.Create;

public class CreateBookCommandHandler : ICommandHandler<CreateBookCommand, Guid>
{
    private readonly IBookRepository _bookRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICategoryRepository _categoryRepository;

    public CreateBookCommandHandler(IBookRepository bookRepository, IUnitOfWork unitOfWork, ICategoryRepository categoryRepository)
    {
        _bookRepository = bookRepository;
        _unitOfWork = unitOfWork;
        _categoryRepository = categoryRepository;
    }

    public async Task<Result<Guid>> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.FindByIdAsync(request.CategoryId);

        if (category is null)
        {
            return Result.Failure<Guid>(CategoryErrors.CategoryNotFound(request.CategoryId));
        }

        var book = Book.Create(
            new Title(request.Title),
            new Author(request.AuthorName, request.AuthorLastName, request.AuthorDoB),
            request.PublishDate,
            request.CategoryId);

        category.Add(book.Value);

        _bookRepository.Add(book.Value);
       
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return book.Value.Id;
    }
}
