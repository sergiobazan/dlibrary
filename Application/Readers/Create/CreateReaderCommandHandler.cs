using Domain.Abstractions;
using Domain.Reader;
using MediatR;

namespace Application.Readers.Create;

public sealed class CreateReaderCommandHandler : IRequestHandler<CreateReaderCommand>
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

        if (reader is null)
        {
            throw new ReaderException();
        }

        _readerRepository.Add(reader);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
      
    }
}
