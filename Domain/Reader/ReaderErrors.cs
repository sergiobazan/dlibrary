using Domain.Abstractions;

namespace Domain.Reader;

public static class ReaderErrors
{
    public static Error ReaderNotFound(Guid id) => new(
        "Readers.NotFound", $"Reader with Id = {id} was not found");

    public static Error LoanDaysOutOfRange() => new(
       "Readers.LoanDaysOutOfRange", $"The number of days should be between 0 and 5");
}
