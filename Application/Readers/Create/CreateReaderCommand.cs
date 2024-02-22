using MediatR;

namespace Application.Readers.Create;

public sealed record CreateReaderCommand(string Name, string LastName, string Email) : IRequest;
