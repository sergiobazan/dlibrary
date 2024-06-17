using Application.Abstractions.Behavior;
using Application.Abstractions.Behavior.Messaging;
using Domain.Abstractions;
using Domain.Reader;

namespace Application.Readers.Create;

public sealed class CreateReaderCommandHandler : ICommandHandler<CreateReaderCommand, string>
{
    private readonly IReaderRepository _readerRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IJwtProvider _jwtProvider;
    private readonly IUnitOfWork _unitOfWork;

    public CreateReaderCommandHandler(
        IReaderRepository readerRepository,
        IUnitOfWork unitOfWork,
        IRoleRepository roleRepository,
        IJwtProvider jwtProvider)
    {
        _readerRepository = readerRepository;
        _unitOfWork = unitOfWork;
        _roleRepository = roleRepository;
        _jwtProvider = jwtProvider;
    }

    public async Task<Result<string>> Handle(CreateReaderCommand request, CancellationToken cancellationToken)
    {
        var reader = Reader.Create(
            new FullName(request.Name, request.LastName),
            Email.Create(request.Email));

        _readerRepository.Add(reader.Value);

        Role? role;

        role = await _roleRepository.GetRoleAsync("Reader");

        role ??= await _roleRepository.CreateAsync("Reader");

        reader.Value.AddRole(role);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var jwt = await _jwtProvider.GenerateAsync(reader.Value);

        return jwt;
    }
}
