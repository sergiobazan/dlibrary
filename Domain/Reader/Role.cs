namespace Domain.Reader;

public sealed class Role
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<Reader> Readers { get; set; }
}
