using Domain.Abstractions;
using Domain.Books;


namespace Domain.Categories;

public class Category : Entity
{
    private Category()
    {
    }

    private Category(
        Guid id,
        Name? name,
        Description? description) 
        : base(id)
    {
        Name = name;
        Description = description;
    }

    public Name? Name { get; private set; }
    public Description? Description { get; private set; }
    private readonly List<Book> _books = new();
    public IReadOnlyList<Book> Books => _books;

    public static Result<Category> Create(Name? name, Description? description)
    {
        var category = new Category(Guid.NewGuid(), name, description);

        return category;
    }

    public void Add(Book book)
    {
        _books.Add(book);
    }
}
