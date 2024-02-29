using Application.Abstractions.Behavior;
using Application.Abstractions.Behavior.Messaging;
using Domain.Abstractions;
using Domain.Reader;

namespace Application.Readers.Login;

public sealed class LoginCommandHandler : ICommandHandler<LoginCommand, string>
{
    private readonly IReaderRepository _readerRepository;
    private readonly IJwtProvider _jwtProvider;
    public LoginCommandHandler(IReaderRepository readerRepository, IJwtProvider jwtProvider)
    {
        _readerRepository = readerRepository;
        _jwtProvider = jwtProvider;
    }

    public async Task<Result<string>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var email = Email.Create(request.Email);
        var reader = await _readerRepository.GetByEmailAsync(email);

        if (reader is null)
        {
            return Result.Failure<string>(ReaderErrors.InvalidCredentials);
        }

        var token = _jwtProvider.Generate(reader);

        return token;

    }
}
