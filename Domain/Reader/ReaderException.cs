namespace Domain.Reader;

public class ReaderException : Exception
{
    public ReaderException()
        : base($"Reader can't be created")
    {
        
    }
}
