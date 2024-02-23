using Application.Abstractions.Behavior.Messaging;
using Domain.Abstractions;
using Domain.Reader;

namespace Application.Readers.Create;

public sealed class CreateReaderCommandHandler : ICommandHandler<CreateReaderCommand>
{
    private readonly IReaderRepository _readerRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateReaderCommandHandler(IReaderRepository readerRepository, IUnitOfWork unitOfWork)
    {
        _readerRepository = readerRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(CreateReaderCommand request, CancellationToken cancellationToken)
    {
        var reader = Reader.Create(
            new FullName(request.Name, request.LastName),
            Email.Create(request.Email));

        _readerRepository.Add(reader.Value);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
      
    }
}
