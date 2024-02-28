using Domain.Abstractions;

namespace Domain.Books;

public static class BookErrors
{
    public static Error BookIsNotAvailable(Guid id) => new(
        "Books.NotAvailable", $"Book with Id = {id} is not available");
    public static Error BookIsNotBorrow(Guid id) => new(
      "Books.NotBorrow", $"Book with Id = {id} was not borrowed");
    public static Error BookNotFound(Guid id) => new(
       "Books.NotFound", $"Book with Id = {id} was not found");

    public static Error BookTitleNotFound(string title) => new(
       "Books.TitleNotFound", $"Book with Title = {title} was not found");
}
