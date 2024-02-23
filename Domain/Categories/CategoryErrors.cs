using Domain.Abstractions;

namespace Domain.Categories;

public static class CategoryErrors
{
    public static Error CategoryNotFound(Guid id) => new(
        "Category.NotFound", $"Category with Id = {id} was not found");
}
