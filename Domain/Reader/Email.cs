namespace Domain.Reader;

public sealed record Email
{
    public string Value { get; init; }

    private Email(string value) => Value = value;

    public static Email? Create(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return null;
        }

        return new Email(value);
    }
}