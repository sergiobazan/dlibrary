namespace Domain.Reader;

public class CreateReaderException : Exception
{
    public CreateReaderException()
        : base($"Reader can't be created")
    {
        
    }
}
