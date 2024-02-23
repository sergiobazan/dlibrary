using Application.Abstractions.Behavior.Messaging;
using Domain.Abstractions;
using Domain.Categories;

namespace Application.Categories.Create;

public class CreateCategoryCommandHandler : ICommandHandler<CreateCategoryCommand>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateCategoryCommandHandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
    {
        _categoryRepository = categoryRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = Category.Create(new Name(request.name), new Description(request.description));

        _categoryRepository.Add(category.Value);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
