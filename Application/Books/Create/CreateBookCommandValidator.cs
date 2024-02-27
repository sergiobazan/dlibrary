using Domain.Books;
using FluentValidation;

namespace Application.Books.Create;

public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
{
    public CreateBookCommandValidator(IBookRepository bookRepository)
    {
        RuleFor(b => b.CategoryId)
            .NotEmpty()
            .MustAsync(async (category, _) =>
            {
                return await bookRepository.CategoryExistsAsync(category);
            }).WithMessage("Category was not found");
    }
}
