using Domain.Reader;

namespace Application.Abstractions.Behavior;

public interface IJwtProvider
{
    string Generate(Reader reader);
}
