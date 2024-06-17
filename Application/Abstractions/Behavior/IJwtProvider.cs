using Domain.Reader;

namespace Application.Abstractions.Behavior;

public interface IJwtProvider
{
    Task<string> GenerateAsync(Reader reader);
}
